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

    public virtual DbSet<ActivityLogs> ActivityLogs { get; set; }

    public virtual DbSet<Appointments> Appointments { get; set; }

    public virtual DbSet<BloodArticles> BloodArticles { get; set; }

    public virtual DbSet<BloodDonationHistories> BloodDonationHistories { get; set; }

    public virtual DbSet<BloodInventories> BloodInventories { get; set; }

    public virtual DbSet<BloodInventoryHistories> BloodInventoryHistories { get; set; }

    public virtual DbSet<BloodRequests> BloodRequests { get; set; }

    public virtual DbSet<Components> Components { get; set; }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<DonationReminders> DonationReminders { get; set; }

    public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Patients> Patients { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Tags> Tags { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLogs>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E5499A8C528997E");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActivityType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EntityId).HasColumnName("EntityID");
            entity.Property(e => e.EntityType).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__UserI__571DF1D5");
        });

        modelBuilder.Entity<Appointments>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2B557C97A");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.Cancel).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((byte)0);
            entity.Property(e => e.TimeSlot).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__UserI__7A672E12");
        });

        modelBuilder.Entity<BloodArticles>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__BloodArt__9C6270C86D9976F3");

            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BloodArticles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodArti__UserI__45F365D3");

            entity.HasMany(d => d.Tags).WithMany(p => p.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTag",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__TagID__4BAC3F29"),
                    l => l.HasOne<BloodArticles>().WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArticleTa__Artic__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("ArticleId", "TagId").HasName("PK__ArticleT__4A35BF6C35DB5902");
                        j.ToTable("ArticleTags");
                        j.IndexerProperty<int>("ArticleId").HasColumnName("ArticleID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<BloodDonationHistories>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__BloodDon__C5082EDB4BC21583");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.IsSuccess).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Component).WithMany(p => p.BloodDonationHistories)
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("FK__BloodDona__Compo__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.BloodDonationHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodDona__UserI__73BA3083");
        });

        modelBuilder.Entity<BloodInventories>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__BloodInv__F5FDE6D34D37A6CB");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BagType).HasMaxLength(5);
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IsRare).HasDefaultValue(false);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RhType).HasMaxLength(3);

            entity.HasOne(d => d.Component).WithMany(p => p.BloodInventories)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodInve__Compo__5FB337D6");
        });

        modelBuilder.Entity<BloodInventoryHistories>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__BloodInv__4D7B4ADD7B680589");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.ActionType).HasMaxLength(10);
            entity.Property(e => e.BagType).HasMaxLength(5);
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PerformedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.RhType).HasMaxLength(3);

            entity.HasOne(d => d.Component).WithMany(p => p.BloodInventoryHistories)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodInve__Compo__6477ECF3");

            entity.HasOne(d => d.Inventory).WithMany(p => p.BloodInventoryHistories)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__BloodInve__Inven__66603565");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.BloodInventoryHistories)
                .HasForeignKey(d => d.PerformedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodInve__Perfo__656C112C");
        });

        modelBuilder.Entity<BloodRequests>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__BloodReq__33A8519A70D8A02E");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoctorName).HasMaxLength(100);
            entity.Property(e => e.DoctorPhone).HasMaxLength(11);
            entity.Property(e => e.FacilityName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.Relationship).HasMaxLength(20);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Component).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("FK__BloodRequ__Compo__6D0D32F4");

            entity.HasOne(d => d.Patient).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__BloodRequ__Patie__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__6E01572D");
        });

        modelBuilder.Entity<Components>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__Componen__D79CF02EA17E01A7");

            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");
            entity.Property(e => e.ComponentType).HasMaxLength(20);
        });

        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD2C7829C1");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<DonationReminders>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Donation__01A830A7B29CF6FE");

            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.SuggestedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.DonationReminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationR__UserI__03F0984C");
        });

        modelBuilder.Entity<HospitalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hospital__3214EC27783D6BC0");

            entity.ToTable("HospitalInfo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.WorkingHours).HasMaxLength(255);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__News__AA1260385AEA2106");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.PostedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.News)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__News__UserID__4F7CD00D");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "NewsTag",
                    r => r.HasOne<Tags>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NewsTags__TagID__534D60F1"),
                    l => l.HasOne<News>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NewsTags__PostID__52593CB8"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("PK__NewsTags__7C45AF9CF3039B15");
                        j.ToTable("NewsTags");
                        j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32F292990C");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.Priority).HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__00200768");
        });

        modelBuilder.Entity<Patients>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC34629EE787B");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(11);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AB4A94C9A");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160D449C40E").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Tags>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CFA4CCC1D188A");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACBC994C95");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Idcard)
                .HasMaxLength(12)
                .HasColumnName("IDCard");
            entity.Property(e => e.IdcardType)
                .HasMaxLength(50)
                .HasColumnName("IDCardType");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.RhType).HasMaxLength(3);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Ward).HasMaxLength(255);

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Users__Departmen__3F466844");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
