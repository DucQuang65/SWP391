using System;

namespace Hien_mau.Dto;

public class AppointmentDetailDto
{

    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string BloodGroup { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string RhType { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
  
    public byte? Status { get; set; }
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string TimeSlot { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Notes { get; set; }
    public bool? Cancel { get; set; }
}