using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public byte? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
