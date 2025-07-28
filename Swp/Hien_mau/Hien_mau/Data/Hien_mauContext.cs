    using Azure;
    using Hien_mau.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;

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
        public virtual DbSet<BloodInventoryHistories> BloodInventoryHistories { get; set; }

        public virtual DbSet<ActivityLogs> ActivityLogs { get; set; }

        public virtual DbSet<Appointments> Appointments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        public virtual DbSet<BloodInventories> BloodInventories { get; set; }

        public virtual DbSet<BloodRequests> BloodRequests { get; set; }

        public virtual DbSet<Contents> Contents { get; set; }

     

        public virtual DbSet<HospitalInfo> HospitalInfos { get; set; }

        public virtual DbSet<Notifications> Notifications { get; set; }

        public virtual DbSet<PublicBloodRequest> PublicBloodRequests { get; set; }

        public virtual DbSet<RequestComponent> RequestComponents { get; set; }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<Tags> Tags { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Patients> Patients { get; set; }

        public virtual DbSet<Components> Components { get; set; }
        public virtual DbSet<UploadedFile> UploadedFiles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Name=MyDB", sqlOptions =>
                sqlOptions.UseCompatibilityLevel(120))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        modelBuilder.Entity<Components>(entity =>
        {
            entity.ToTable("Components");
            entity.HasKey(e => e.ComponentId);
            entity.Property(e => e.ComponentType).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Appointments>(entity =>
        {
            entity.HasKey(e => e.AppointmentID);
            entity.Property(e => e.AppointmentID).HasColumnName("AppointmentID").ValueGeneratedOnAdd();
            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.DoctorID1).HasColumnName("DoctorID1");
            entity.Property(e => e.DoctorID2).HasColumnName("DoctorID2");
            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.TimeSlot).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.BloodPressure).HasMaxLength(20);
            entity.Property(e => e.HeartRate);
            entity.Property(e => e.Hemoglobin);
            entity.Property(e => e.Temperature);
            entity.Property(e => e.Status).HasColumnName("Status");
            entity.Property(e => e.Cancel).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.WeightAppointment).HasColumnType("float");
            entity.Property(e => e.HeightAppointment).HasColumnType("float");
            entity.Property(e => e.DonationCapacity).HasColumnType("float").HasColumnName("DonationCapacity");
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.BloodGroup).HasMaxLength(2);
            entity.Property(e => e.RhType).HasMaxLength(3);


            entity.HasOne(e => e.User)
                 .WithMany(u => u.Appointments)
                 .HasForeignKey(e => e.UserID)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Appointments_Users");

            entity.HasOne(e => e.Doctor1)
                 .WithMany()
                 .HasForeignKey(e => e.DoctorID1)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Appointments_Doctor1");

            entity.HasOne(e => e.Doctor2)
                 .WithMany()
                 .HasForeignKey(e => e.DoctorID2)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Appointments_Doctor2");
        });


        modelBuilder.Entity<BloodInventories>(entity =>
        {
            entity.HasKey(e => e.InventoryId);

            entity.Property(e => e.InventoryId)
                .HasColumnName("InventoryID");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(2)
                .IsRequired();

            entity.Property(e => e.RhType)
                .HasMaxLength(3)
                .IsRequired();

            entity.Property(e => e.BagType)
                .HasMaxLength(5)
                .IsRequired();

            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.IsRare)
                .HasDefaultValue(false)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.LastUpdated)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ComponentId)
                .HasColumnName("ComponentID");

            entity.HasOne(e => e.Components)
                .WithMany(c => c.BloodInventories)
                .HasForeignKey(e => e.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodInventories_Components");

            entity.HasMany(e => e.BloodInventoryHistories)
                .WithOne(h => h.Inventory)
                .HasForeignKey(h => h.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BloodInventoryHistories>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.ActionType).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.PerformedAt)
                      .HasDefaultValueSql("(getdate())")
                      .HasColumnType("datetime");
                entity.Property(e => e.BagType).HasMaxLength(10);
                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(e => e.Inventory)
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

            entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

            entity.HasOne(d => d.Components)
                .WithMany(c => c.BloodRequests) 
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BloodRequests_Components");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BloodRequ__UserI__7E37BEF6");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.ToTable("Reminders");
            entity.HasKey(r => r.ReminderId);
            entity.Property(r => r.Type).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Message).IsRequired().HasMaxLength(255);
            entity.Property(r => r.UserId).HasColumnName("UserID"); 
            entity.HasOne(r => r.User)
                  .WithMany(u => u.Reminders)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity<UploadedFile>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Uploaded__3214EC078BEAE4EF");

                entity.Property(e => e.FileName).HasMaxLength(255);
                entity.Property(e => e.FileUrl).HasMaxLength(500);
                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });
        OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
