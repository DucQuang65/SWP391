using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class DonationReminder
{
    [Key]
    public int ReminderId { get; set; }

    public int UserId { get; set; }

    public DateTime SuggestedDate { get; set; }

    public bool? IsSent { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual User User { get; set; } = null!;
}
