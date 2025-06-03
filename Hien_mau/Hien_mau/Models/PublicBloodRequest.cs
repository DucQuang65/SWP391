using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class PublicBloodRequest
{
    public int PublicRequestId { get; set; }

    public int RequestId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime Deadline { get; set; }

    public bool? IsRare { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual BloodRequest Request { get; set; } = null!;
}
