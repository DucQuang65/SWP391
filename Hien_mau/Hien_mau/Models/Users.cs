using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models;

public partial class Users
{
    [Key]
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Phone { get; set; }

    public string? IdcardType { get; set; }

    public string? Idcard { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public string? Address { get; set; }

    public double? Distance { get; set; }

    public string? BloodGroup { get; set; }

    public string? RhType { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public byte? Status { get; set; }

    public int RoleId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? SelfReportedLastDonationDate { get; set; }

    public virtual ICollection<ActivityLogs> ActivityLogs { get; set; } = new List<ActivityLogs>();

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();

    [InverseProperty(nameof(BloodDonationHistories.Doctor))]
    public virtual ICollection<BloodDonationHistories> BloodDonationsAsDoctor { get; set; } = new List<BloodDonationHistories>();



    public virtual ICollection<BloodInventoryHistories> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistories>();

    public virtual ICollection<BloodRequests> BloodRequests { get; set; } = new List<BloodRequests>();

    public virtual Departments? Department { get; set; }


    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();


    public virtual ICollection<Contents> Contents { get; set; } = new List<Contents>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual Roles Role { get; set; } = null!;
}
