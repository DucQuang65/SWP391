using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models;

public partial class BloodDonationHistories
{
    [Key]
    public int DonationId { get; set; }
   
    public int AppointmentId { get; set; }
    public DateTime DonationDate { get; set; }
    public string? BloodGroup { get; set; } = null!;
    public string? RhType { get; set; } = null!;
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSuccess { get; set; } = false;
    
    
    [ForeignKey(nameof(AppointmentId))]
    public Appointments? Appointment { get; set; }

    [ForeignKey(nameof(DoctorId))]
    [InverseProperty(nameof(Users.BloodDonationsAsDoctor))]
    public Users? Doctor { get; set; }

}
