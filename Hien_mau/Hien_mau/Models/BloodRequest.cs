using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodRequest
{
    [Key]
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public int Quantity { get; set; }

    public byte UrgencyLevel { get; set; }

    public DateTime NeededTime { get; set; }

    public string? Reason { get; set; }

    public bool? IsAutoApproved { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedTime { get; set; }

    public virtual ICollection<BloodRequestHistory> BloodRequestHistories { get; set; } = new List<BloodRequestHistory>();

    public virtual ICollection<PublicBloodRequest> PublicBloodRequests { get; set; } = new List<PublicBloodRequest>();

    public virtual ICollection<RequestComponent> RequestComponents { get; set; } = new List<RequestComponent>();

    public virtual User User { get; set; } = null!;
}
