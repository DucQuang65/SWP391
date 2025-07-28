using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Notifications
{
    [Key]
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool? Priority { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual Users User { get; set; } = null!;
}
