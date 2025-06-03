using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class BloodDonationHistory
{
    public int DonationId { get; set; }

    public int UserId { get; set; }

    public DateTime DonationDate { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public string ComponentType { get; set; } = null!;

    public int Quantity { get; set; }

    public bool? IsSuccessful { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
