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

  

    public virtual DbSet<BloodDonationHistories> BloodDonationHistories { get; set; }

    public virtual DbSet<BloodInventories> BloodInventories { get; set; }

    public virtual DbSet<BloodInventoryHistories> BloodInventoryHistories { get; set; }

    public virtual DbSet<BloodRequests> BloodRequests { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Departments> Departments { get; set; }

   
    public virtual DbSet<Reminder> Reminders { get; set; } 
    public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

    public virtual DbSet<Contents> News { get; set; }

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

       

        modelBuilder.Entity<BloodDonationHistories>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__BloodDon__C5082EDB4BC21583");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID").IsRequired();
            entity.Property(e => e.DonationDate).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");

            entity.HasOne(d => d.Appointment)
                .WithMany()
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodDonationHistories_Appointments");

            entity.HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodDonationHistories_Users_Doctor");
        });

        modelBuilder.Entity<BloodInventories>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__BloodInv__F5FDE6D34D37A6CB");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.BloodGroup).HasMaxLength(2).IsRequired();
            entity.Property(e => e.RhType).HasMaxLength(3).IsRequired();
            entity.Property(e => e.BagType).HasMaxLength(5);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.IsRare).HasDefaultValue(false);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.LastUpdated)
                  .HasDefaultValueSql("(getdate())")
                  .HasColumnType("datetime");
            entity.Property(e => e.ComponentId).HasColumnName("ComponentId"); // d thường!



            entity.HasOne(d => d.Component)
                  .WithMany(p => p.BloodInventories)
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
           
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PerformedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
      
            entity.HasOne(d => d.Inventory).WithMany(p => p.BloodInventoryHistories)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__BloodInve__Inven__66603565");

            entity.HasOne(d => d.PerformedByUser)
                .WithMany(p => p.BloodInventoryHistories)
                .HasForeignKey(d => d.PerformedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodInventoryHistories_Users_PerformedBy");
            //entity.Property(e => e.ComponentId).HasColumnName("ComponentID").IsRequired();
            //entity.HasOne(d => d.Component)
                //.WithMany()  // hoặc .WithMany(p => p.BloodInventoryHistories) nếu có collection
                //.HasForeignKey(d => d.ComponentId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK_BloodInventoryHistories_Components");

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

        modelBuilder.Entity<Component>(entity =>
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

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Donation__01A830A7B29CF6FE");

            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");
            entity.Property(e => e.IsSent).HasDefaultValue(false);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
           
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Reminders)
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

        modelBuilder.Entity<Contents>(entity =>
        {
            entity.HasKey(e => e.ContentID).HasName("PK__Contents__A2B0A3D1F5C8E4B6");
            entity.Property(e => e.ContentID).HasColumnName("ContentID");
            entity.Property(e => e.Title).HasColumnName("Title");
            entity.Property(e => e.Content).HasColumnName("Content");
            entity.Property(e => e.ImgUrl).HasColumnName("ImgUrl");
            entity.Property(e => e.ContentType).HasColumnName("ContentType");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.HasOne(d => d.User)
               .WithMany(p => p.Contents)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contents_Users");
            entity.HasMany(d => d.Tags)
              .WithMany(p => p.Contents)
              .UsingEntity<Dictionary<string, object>>(
                 "ContentTags",
                    r => r.HasOne<Tags>()
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                         .HasConstraintName("FK_ContentTags_Tags"),
                    l => l.HasOne<Contents>()
                        .WithMany()
                        .HasForeignKey("ContentID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ContentTags_Contents"),
                    j =>
                    {
                        j.HasKey("ContentID", "TagID").HasName("PK_ContentTags");
                        j.ToTable("ContentTags");
                        j.IndexerProperty<int>("ContentID").HasColumnName("ContentID");
                        j.IndexerProperty<int>("TagID").HasColumnName("TagID");
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
