using System;
using System.Collections.Generic;
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
    public virtual DbSet<BloodInventoryHistory> BloodInventoryHistory { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<BloodArticle> BloodArticles { get; set; }

    public virtual DbSet<BloodDonationHistory> BloodDonationHistory { get; set; }

    public virtual DbSet<BloodInventory> BloodInventory { get; set; }

    public virtual DbSet<BloodRequest> BloodRequests { get; set; }

    public virtual DbSet<BloodRequestHistory> BloodRequestHistories { get; set; }

    public virtual DbSet<DonationReminder> DonationReminders { get; set; }

    public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PublicBloodRequest> PublicBloodRequests { get; set; }

    public virtual DbSet<RequestComponent> RequestComponents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<BloodInventoryHistory>().ToTable("BloodInventoryHistory");
        modelBuilder.Entity<BloodInventory>().ToTable("BloodInventory");
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId);
            entity.Property(e => e.UserID);
            entity.Property(e => e.ActivityType);
            entity.Property(e => e.EntityType);
            entity.Property(e => e.Description);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_Users");
        });
        modelBuilder.Entity<Appointment>(entity =>
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

        modelBuilder.Entity<BloodArticle>(entity =>
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
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__TagID__6C190EBB"),
                    l => l.HasOne<BloodArticle>().WithMany()
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

        modelBuilder.Entity<BloodDonationHistory>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__BloodDon__C5082EDB37B9BA37");

            entity.ToTable("BloodDonationHistory");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentType).HasMaxLength(20);
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.IsSuccessful).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodDonationHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodDona__UserI__0A9D95DB");
        });

        modelBuilder.Entity<BloodInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryID);
            entity.Property(e => e.InventoryID).HasColumnName("InventoryID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.ComponentType).HasMaxLength(20).IsRequired();
            entity.Property(e => e.BagType).HasMaxLength(5);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.IsRare).HasDefaultValue(false).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasMany(e => e.Histories)
                  .WithOne(h => h.BloodInventory)
                  .HasForeignKey(h => h.InventoryID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BloodInventoryHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryID);
            entity.ToTable("BloodInventoryHistory");
            entity.Property(e => e.InventoryID);
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.ComponentType).HasMaxLength(20).IsRequired();
            entity.Property(e => e.ActionType).HasMaxLength(10).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PerformedBy).HasColumnName("PerformedBy");
            entity.Property(e => e.PerformedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.BagType).HasMaxLength(10);
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasOne(e => e.BloodInventory)
                  .WithMany(b => b.Histories)
                  .HasForeignKey(e => e.InventoryID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.PerformedByUser)
                  .WithMany(u => u.BloodInventoryHistories)
                  .HasForeignKey(e => e.PerformedBy)
                  .HasPrincipalKey(u => u.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BloodRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__BloodReq__33A8519A48D7A9B8");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAutoApproved).HasDefaultValue(false);
            entity.Property(e => e.NeededTime).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__7E37BEF6");
        });

        modelBuilder.Entity<BloodRequestHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__BloodReq__4D7B4ADDD8C51D74");

            entity.ToTable("BloodRequestHistory");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Request).WithMany(p => p.BloodRequestHistories)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__Reque__04E4BC85");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequestHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__05D8E0BE");
        });

        modelBuilder.Entity<DonationReminder>(entity =>
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
                    r => r.HasOne<Tag>().WithMany()
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

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E320425465A");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.Priority).HasDefaultValue((byte)0);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__1EA48E88");
        });

        modelBuilder.Entity<PublicBloodRequest>(entity =>
        {
            entity.HasKey(e => e.PublicRequestId).HasName("PK__PublicBl__FF814DC16BE6E91E");

            entity.Property(e => e.PublicRequestId).HasColumnName("PublicRequestID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.IsRare).HasDefaultValue(false);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.RhType).HasMaxLength(3);

            entity.HasOne(d => d.Request).WithMany(p => p.PublicBloodRequests)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PublicBlo__Reque__10566F31");
        });

        modelBuilder.Entity<RequestComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__RequestC__D79CF02E56311009");

            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ComponentType).HasMaxLength(20);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestComponents)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCo__Reque__01142BA1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A2C6F1841");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160DF607C36").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4C22818AFF");

            entity.Property(e => e.TagId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
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
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__60A75C0F");
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
