using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hien_mau.Data;
using Hien_mau.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace Hien_mau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5101/", "http://localhost:5173/", "http://localhost:3000") // FE domain
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); // If use cookie                          
                    });
            });
            //// Register FirebaseAdmin with google-services.json
            //builder.Services.AddSingleton(provider =>
            //{
            //    var configuration = provider.GetRequiredService<IConfiguration>();
            //    var projectRoot = Directory.GetCurrentDirectory();
            //    var googleServicesPath = Path.Combine(projectRoot, "google-services.json");

            //    if (!File.Exists(googleServicesPath))
            //    {
            //        throw new FileNotFoundException("google-services.json not found in project root");
            //    }

            //    var serviceAccountJson = File.ReadAllText(googleServicesPath);
            //    var projectId = configuration["Firebase:ProjectId"]
            //        ?? throw new InvalidOperationException("Firebase ProjectId is missing");

            //    var firebaseApp = FirebaseApp.Create(new AppOptions
            //    {
            //        Credential = GoogleCredential.FromJson(serviceAccountJson),
            //        ProjectId = projectId
            //    });
            //    return firebaseApp;
            //});

            builder.Services.AddDbContext<Hien_mauContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));

            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HienMau API v1");
                c.RoutePrefix = "";
            });

            app.UseCors("_myAllowSpecificOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
