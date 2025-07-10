using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodDonationHistories
{
    [Key]
    public int DonationId { get; set; }
   
    public int AppointmentId { get; set; }
    public DateTime DonationDate { get; set; }
    public string BloodGroup { get; set; } = null!;
    public string RhType { get; set; } = null!;
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSuccess { get; set; } = false;
    public Appointments? Appointment { get; set; }
    public Users? Doctor { get; set; }

}
