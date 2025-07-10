using System;
using System.Collections.Generic;
using System.Data;
using Hien_mau.Models;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Data;

public partial class Hien_mauContext : DbContext
{
    public Hien_mauContext()
    {
    }

    public Hien_mauContext(DbContextOptions<Hien_mauContext> options)
        : base(options)
    {
    }
    public virtual DbSet<BloodInventoryHistories> BloodInventoryHistory { get; set; }

    public virtual DbSet<ActivityLogs> ActivityLogs { get; set; }

    public virtual DbSet<Appointments> Appointments { get; set; }

    public virtual DbSet<BloodArticles> BloodArticles { get; set; }

    public virtual DbSet<BloodDonationHistories> BloodDonationHistory { get; set; }

    public virtual DbSet<BloodInventories> BloodInventory { get; set; }

    public virtual DbSet<BloodRequests> BloodRequests { get; set; }

    public virtual DbSet<DonationReminders> DonationReminders { get; set; }

    public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<PublicBloodRequest> PublicBloodRequests { get; set; }

    public virtual DbSet<RequestComponent> RequestComponents { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Tags> Tags { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public DbSet<Patients> Patients { get; set; }

    //public virtual DbSet<UserLocation> UserLocations { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=MyDB", sqlOptions =>
            sqlOptions.UseCompatibilityLevel(120))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<BloodInventoryHistories>().ToTable("BloodInventoryHistory");
        modelBuilder.Entity<BloodInventories>().ToTable("BloodInventory");
        modelBuilder.Entity<ActivityLogs>(entity =>
        {
            entity.HasKey(e => e.LogId);
            entity.Property(e => e.UserId);
            entity.Property(e => e.ActivityType);
            entity.Property(e => e.EntityType);
            entity.Property(e => e.Description);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_Users");
        });
        modelBuilder.Entity<Appointments>(entity =>
        {
            entity.HasKey(e => e.AppointmentId);
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.TimeSlot).HasMaxLength(50);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.LastDonationDate).HasColumnType("date");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Users");
        });

        modelBuilder.Entity<BloodArticles>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__BloodArt__9C6270C827BEB99B");

            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.ImgUrl).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodArticles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodArti__UserI__66603565");

            entity.HasMany(d => d.Tags).WithMany(p => p.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTag",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__TagID__6C190EBB"),
                    l => l.HasOne<BloodArticles>().WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__Artic__6B24EA82"),
                    j =>
                    {
                        j.HasKey("ArticleId", "TagId").HasName("PK__ArticleT__4A35BF6C47BC486E");
                        j.ToTable("ArticleTags");
                        j.IndexerProperty<int>("ArticleId").HasColumnName("ArticleID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<BloodDonationHistories>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__BloodDon__C5082EDB37B9BA37");

            entity.ToTable("BloodDonationHistory");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodDonationHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodDona__UserI__0A9D95DB");
        });

        modelBuilder.Entity<BloodInventories>(entity =>
        {
            entity.HasKey(e => e.InventoryId);
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.BagType).HasMaxLength(5);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.IsRare).HasDefaultValue(false).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasMany(e => e.BloodInventoryHistories)
                  .WithOne(h => h.BloodInventory)
                  .HasForeignKey(h => h.InventoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BloodInventoryHistories>(entity =>
        {
            entity.HasKey(e => e.HistoryId);
            entity.ToTable("BloodInventoryHistory");
            entity.Property(e => e.InventoryId);
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.ActionType).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PerformedBy).HasColumnName("PerformedBy");
            entity.Property(e => e.PerformedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.BagType).HasMaxLength(10);
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasOne(e => e.BloodInventory)
                  .WithMany(b => b.BloodInventoryHistories)
                  .HasForeignKey(e => e.InventoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.PerformedByUser)
                  .WithMany(u => u.BloodInventoryHistories)
                  .HasForeignKey(e => e.PerformedBy)
                  .HasPrincipalKey(u => u.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BloodRequests>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__BloodReq__33A8519A48D7A9B8");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__7E37BEF6");
        });

        modelBuilder.Entity<DonationReminders>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Donation__01A830A714C2E1D9");

            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.SuggestedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.DonationReminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationR__UserI__22751F6C");
        });

        modelBuilder.Entity<HospitalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hospital__3214EC27AA656362");

            entity.ToTable("HospitalInfo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.MapImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.WorkingHours).HasMaxLength(255);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__News__AA126038FB3E3E48");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ImgUrl).HasMaxLength(255);
            entity.Property(e => e.PostedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.News)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__UserID__6FE99F9F");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "NewsTag",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NewsTags__TagID__73BA3083"),
                    l => l.HasOne<News>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NewsTags__PostID__72C60C4A"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__NewsTags__7C45AF9C069C599E");
                        j.ToTable("NewsTags");
                        j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        //modelBuilder.Entity<Notifications>(entity =>
        //{
        //    entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E320425465A");

        //    entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
        //    entity.Property(e => e.IsRead).HasDefaultValue(false);
        //    entity.Property(e => e.Message).HasMaxLength(255);
        //    entity.Property(e => e.Priority).HasDefaultValue((byte)0);
        //    entity.Property(e => e.SentAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.Title).HasMaxLength(255);
        //    entity.Property(e => e.Type).HasMaxLength(50);
        //    entity.Property(e => e.UserId).HasColumnName("UserID");

        //    entity.HasOne(d => d.User).WithMany(p => p.Notifications)
        //        .HasForeignKey(d => d.UserId)
        //        .OnDelete(DeleteBehavior.ClientSetNull)
        //        .HasConstraintName("FK__Notificat__UserI__1EA48E88");
        //});

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A2C6F1841");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160DF607C36").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4C22818AFF");

            entity.Property(e => e.TagId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC841FE99C");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.IdcardType).HasMaxLength(50).HasColumnName("IDCardType");
            entity.Property(e => e.Idcard).HasMaxLength(12).HasColumnName("IDCard");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Age).HasColumnName("Age");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.Ward).HasMaxLength(255);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Distance).HasColumnName("Distance");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.Weight).HasColumnType("float");
            entity.Property(e => e.Height).HasColumnType("float");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");

            entity.HasOne(d => d.Role)
                  .WithMany(p => p.Users)
                  .HasForeignKey(d => d.RoleId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Users__RoleID__60A75C0F");

            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Users)
                  .HasForeignKey(e => e.DepartmentId)
                  .HasConstraintName("FK__Users__DepartmentID");
        });
        //modelBuilder.Entity<Reminder>(entity =>
        //{
        //    entity.HasKey(e => e.ReminderId);
        //    entity.Property(e => e.Type).HasMaxLength(100);
        //    entity.Property(e => e.Message).IsRequired();
        //    entity.Property(e => e.RemindAt).HasColumnType("datetime");
        //    entity.Property(e => e.IsDisabled).HasDefaultValue(false);
        //    entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");

        //    entity.Property(e => e.UserId).HasColumnName("UserID");

        //    entity.HasOne(e => e.User)
        //          .WithMany(u => u.Reminders)
        //          .HasForeignKey(e => e.UserId)
        //          .OnDelete(DeleteBehavior.ClientSetNull)
        //          .HasConstraintName("FK_Reminders_Users");
        //});


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}