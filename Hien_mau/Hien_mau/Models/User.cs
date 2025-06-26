using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class User
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

    public float? Distance { get; set; }

    public string? BloodGroup { get; set; }

    public string? RhType { get; set; }

    public float? Weight { get; set; }

    public float? Height { get; set; }

    public byte Status { get; set; }

    public int RoleId { get; set; }

    public string? Department { get; set; }

    public DateTime? CreatedAt { get; set; }
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<BloodArticle> BloodArticles { get; set; } = new List<BloodArticle>();

    public virtual ICollection<BloodDonationHistory> BloodDonationHistories { get; set; } = new List<BloodDonationHistory>();

    public virtual ICollection<BloodRequestHistory> BloodRequestHistories { get; set; } = new List<BloodRequestHistory>();

    public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    public virtual ICollection<DonationReminder> DonationReminders { get; set; } = new List<DonationReminder>();

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Role Role { get; set; } = null!;

    public ICollection<BloodInventoryHistory> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistory>();


}
