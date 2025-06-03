using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Models;

public partial class BloodManagementSystemContext : DbContext
{
    public BloodManagementSystemContext()
    {
    }

    public BloodManagementSystemContext(DbContextOptions<BloodManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<BloodArticle> BloodArticles { get; set; }

    public virtual DbSet<BloodDonationHistory> BloodDonationHistories { get; set; }

    public virtual DbSet<BloodInventory> BloodInventories { get; set; }

    public virtual DbSet<BloodRequest> BloodRequests { get; set; }

    public virtual DbSet<BloodRequestHistory> BloodRequestHistories { get; set; }

    public virtual DbSet<DonationReminder> DonationReminders { get; set; }

    public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PublicBloodRequest> PublicBloodRequests { get; set; }

    public virtual DbSet<RequestComponent> RequestComponents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLocation> UserLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA248BC208F");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((byte)0);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__UserI__74AE54BC");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA126038D69277F9");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ImgUrl).HasMaxLength(255);
            entity.Property(e => e.PostedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue((byte)0);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPosts__UserI__4BAC3F29");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BlogPostT__TagID__02084FDA"),
                    l => l.HasOne<BlogPost>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BlogPostT__PostI__01142BA1"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__BlogPost__7C45AF9C5D5EED99");
                        j.ToTable("BlogPostTags");
                        j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<BloodArticle>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__BloodArt__9C6270C8E60F5E39");

            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.ImgUrl).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Tags).WithMany(p => p.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__TagID__4F7CD00D"),
                    l => l.HasOne<BloodArticle>().WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__Artic__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("ArticleId", "TagId").HasName("PK__ArticleT__4A35BF6CFE507554");
                        j.ToTable("ArticleTags");
                        j.IndexerProperty<int>("ArticleId").HasColumnName("ArticleID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<BloodDonationHistory>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__BloodDon__C5082EDBB97D9CD0");

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
                .HasConstraintName("FK__BloodDona__UserI__66603565");
        });

        modelBuilder.Entity<BloodInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__BloodInv__F5FDE6D3EF129F14");

            entity.ToTable("BloodInventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentType).HasMaxLength(20);
            entity.Property(e => e.IsRare).HasDefaultValue(false);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RhType).HasMaxLength(3);
        });

        modelBuilder.Entity<BloodRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__BloodReq__33A8519A0245FA7D");

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
                .HasConstraintName("FK__BloodRequ__UserI__59FA5E80");
        });

        modelBuilder.Entity<BloodRequestHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__BloodReq__4D7B4ADDD92A8CAE");

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
                .HasConstraintName("FK__BloodRequ__Reque__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequestHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__619B8048");
        });

        modelBuilder.Entity<DonationReminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Donation__01A830A76A4F9CBD");

            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.SuggestedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.DonationReminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationR__UserI__7E37BEF6");
        });

        modelBuilder.Entity<HospitalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hospital__3214EC2751BEFE1F");

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

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E326DC63B38");

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
                .HasConstraintName("FK__Notificat__UserI__7A672E12");
        });

        modelBuilder.Entity<PublicBloodRequest>(entity =>
        {
            entity.HasKey(e => e.PublicRequestId).HasName("PK__PublicBl__FF814DC17CB0AFCA");

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
                .HasConstraintName("FK__PublicBlo__Reque__6C190EBB");
        });

        modelBuilder.Entity<RequestComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__RequestC__D79CF02EDEC07368");

            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ComponentType).HasMaxLength(20);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestComponents)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCo__Reque__5CD6CB2B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A0BA7C21B");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61609E63B8F6").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4C820929B2");

            entity.Property(e => e.TagId)
                .ValueGeneratedNever()
                .HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACEE8C1751");

            entity.HasIndex(e => e.FirebaseUid, "UQ__Users__F8143ECAB0E99BC2").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.FirebaseUid)
                .HasMaxLength(128)
                .HasColumnName("FirebaseUID");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__3C69FB99");
        });

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__UserLoca__E7FEA477A217B1FC");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserLocat__UserI__6FE99F9F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
