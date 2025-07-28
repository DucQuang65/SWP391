using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodRequests
{
    [Key]
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public int? PatientId { get; set; }

    public string? PatientName { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Relationship { get; set; }

    public string? FacilityName { get; set; }

    public string? DoctorName { get; set; }

    public string? DoctorPhone { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public int? ComponentId { get; set; }

    public int Quantity { get; set; }

    public string? Reason { get; set; }

    public byte Status { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? MedicalReport { get; set; }

    public virtual Components Components { get; set; }

    public virtual Patients Patient { get; set; }

    public virtual Users User { get; set; } = null!;    
}
