using System;

namespace Hien_mau.Dto;

public class AppointmentCreateDTO
{
    public int UserId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? TimeSlot { get; set; }
}
public class AppointmentDTO

{
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? TimeSlot { get; set; }
    public byte Status { get; set; }
    public string? Notes { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public int? DoctorId { get; set; }
    public string? BloodPressure { get; set; }
    public bool Cancel { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class DoctorUpdateDTO
{
    public string? Notes { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public int DoctorId { get; set; }
}
public class AppointmentLastDonationDTO
{
    public bool HasDonationHistory { get; set; } // Có record trong hệ thống không
    public DateTime? LastDonationDate { get; set; } // Ngày hiến gần nhất
    public bool IsEditable { get; set; } // Có thể chỉnh sửa không
}