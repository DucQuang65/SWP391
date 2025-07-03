using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models;

public partial class BloodRequest
{
    [Key]
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public int? PatientID { get; set; }
    public string? PatientName { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Relationship { get; set; }
    public string? FacilityName { get; set; }
    public string? DoctorName { get; set; }
    public string? DoctorPhone { get; set; }
    public string BloodGroup { get; set; } = null!;
    public string RhType { get; set; } = null!;
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public bool IsAutoApproved { get; set; } = false;
    public byte Status { get; set; } = 0; 
    public DateTime CreatedTime { get; set; } 

    public virtual ICollection<BloodRequestHistory> BloodRequestHistories { get; set; } = new List<BloodRequestHistory>();

    public virtual ICollection<PublicBloodRequest> PublicBloodRequests { get; set; } = new List<PublicBloodRequest>();

    public virtual ICollection<RequestComponent> RequestComponents { get; set; } = new List<RequestComponent>();

    public virtual User User { get; set; } = null!;
    public virtual Patient Patient { get; set; }
}
