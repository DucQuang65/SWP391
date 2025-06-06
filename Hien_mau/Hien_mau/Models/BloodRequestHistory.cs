﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodRequestHistory
{
    [Key]
    public int HistoryId { get; set; }

    public int RequestId { get; set; }

    public int UserId { get; set; }

    public byte Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual BloodRequest Request { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
