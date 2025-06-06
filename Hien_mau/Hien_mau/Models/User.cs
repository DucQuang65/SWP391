using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string FirebaseUid { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? BloodGroup { get; set; }

    public string? RhType { get; set; }

    public byte Status { get; set; }

    public int RoleId { get; set; }

    public string? Department { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    public virtual ICollection<BloodDonationHistory> BloodDonationHistories { get; set; } = new List<BloodDonationHistory>();

    public virtual ICollection<BloodRequestHistory> BloodRequestHistories { get; set; } = new List<BloodRequestHistory>();

    public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    public virtual ICollection<DonationReminder> DonationReminders { get; set; } = new List<DonationReminder>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserLocation> UserLocations { get; set; } = new List<UserLocation>();
}
