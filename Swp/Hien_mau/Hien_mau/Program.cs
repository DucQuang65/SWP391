using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hien_mau.Data;
using Hien_mau.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Hangfire;
using Hien_mau.Interface;



namespace Hien_mau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Ensure linked with appsettings.json
            builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HienMau API",
                    Version = "v1",
                    Description = "Swagger HienMau UI trong .NET 9"
                });
            });
            const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Get CorsOrigins from appsettings
            var corsOriginsString = builder.Configuration["Jwt:CorsOrigins"];
            string[] corsOrigins = corsOriginsString?.Split(',') ?? Array.Empty<string>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins(corsOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });


            builder.Services.AddDbContext<Hien_mauContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("MyDB")));
            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<BloodInventoryExpiryJob>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<NotificationLog>();
            builder.Services.AddScoped<ActivityLogger>();
            builder.Services.AddTransient<ReminderJob>();
            builder.Services.AddScoped<AppointmentCompletionJob>();
            builder.Services.AddScoped<ISendEmail, SendEmail>();
            builder.Services.AddScoped<IBloodRequestService, BloodRequestService>();
            builder.Services.AddScoped<IInformationService, InformationService>();
            builder.Services.AddScoped<IHospitalInfo, HospitalInfoService>();
            builder.Services.AddHostedService<ReminderEmailJob>();

            //builder.Services.AddHostedService<CancelExpiredService>();




            // Thêm authentication và authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
                options.SaveTokens = true;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {                   
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    //IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    //System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    IssuerSigningKey = GetIssuerSigningKey(builder.Configuration)
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<BloodInventoryExpiryJob>(
                    "cancel-expired-blood",
                    job => job.CheckAndExpireBlood(),   
                    "*/10 * * * *"   // chạy mỗi 10 phút
                );

            RecurringJob.AddOrUpdate<ReminderJob>(
                    "reminder-job",
                    job => job.Run(),
                    "*/10 * * * *" // mỗi 10 phút
                );
            RecurringJob.AddOrUpdate<AppointmentCompletionJob>(
                    "recovery-reminder-job",
                    job => job.Run(),
                    "*/10 * * * *" // Chạy mỗi 10 phút
                );

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HienMau API v1");
                c.RoutePrefix = "";
            });

            app.UseCors("_myAllowSpecificOrigins");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
        private static SecurityKey GetIssuerSigningKey(IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("Jwt:Key is missing in configuration.");
            }
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        }
    }
}
