using System;

namespace Hien_mau.Dto;

public class AppointmentCreateDTO
{
    public int AppointmentId { get; }
    public int UserId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? TimeSlot { get; set; }
    public byte Process { get; set; }
    public bool? Status { get; set; }
    public bool Cancel { get; set; }
}
public class AppointmentDTO

{
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? TimeSlot { get; set; }
    public bool? Status { get; set; }
    public byte Process { get; set; }
    public string? Notes { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public int? DoctorId { get; set; }
    public string? BloodPressure { get; set; }
    public bool Cancel { get; set; }
    public DateTime CreatedAt { get; set; }
    public double? WeightAppointment { get; set; }
    public double? HeightAppointment { get; set; }
    public double? DonationCapacity { get; set; }
}

public class DoctorUpdateDTO
{
    public int AppointmentId { get; }
    public string? Notes { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public float? WeightAppointment { get; set; }
    public float? HeightAppointment { get; set; }
    public int DoctorId { get; set; }
    public byte Process { get; set; }
    public bool? Status { get; set; }
    public double? DonationCapacity {  get; set; }
    }
public class AppointmentLastDonationDTO
{
    public bool HasDonationHistory { get; set; } 
    public DateTime? LastDonationDate { get; set; } 
    public bool IsEditable { get; set; } 
}
public class NoteUpdateDTO
{
    public string Notes { get; set; }
}
