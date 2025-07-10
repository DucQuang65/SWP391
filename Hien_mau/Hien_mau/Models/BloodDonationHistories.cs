using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodDonationHistories
{
    [Key]
    public int DonationId { get; set; }

    public int UserId { get; set; }

    public DateTime DonationDate { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public int? ComponentId { get; set; }

    public int Quantity { get; set; }

    public bool? IsSuccess { get; set; }

    public string? Notes { get; set; }

    public virtual Components? Component { get; set; }

    public virtual Users User { get; set; } = null!;
}
