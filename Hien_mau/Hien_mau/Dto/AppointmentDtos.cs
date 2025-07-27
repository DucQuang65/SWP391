
namespace Hien_mau.Dto;

public class AppointmentDTO
{
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string TimeSlot { get; set; }
    public bool? Status { get; set; }
    public byte Process { get; set; }
    public string? Notes { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public int? DoctorId1 { get; set; }
    public int? DoctorId2 { get; set; }
    public bool Cancel { get; set; }
    public DateTime CreatedAt { get; set; }
    public double? WeightAppointment { get; set; }
    public double? HeightAppointment { get; set; }
    public double? DonationCapacity { get; set; }
    public DateTime? DonationDate { get; set; }
    public string? BloodGroup { get; set; }
    public string? RhType { get; set; }
}

public class AppointmentCreateDTO
{
    public int UserId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; }
    public bool? Status { get; set; }
    public byte Process { get; set; } = 0;
}

public class DoctorHealthCheckDTO
{
    public string? Notes { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public double? WeightAppointment { get; set; }
    public double? HeightAppointment { get; set; }
    public double? DonationCapacity { get; set; }
    public int? DoctorId { get; set; }
    public bool? Status { get; set; }
    public byte Process { get; set; }
}

public class DoctorExaminationDTO
{
    public string? BloodGroup { get; set; }
    public string? RhType { get; set; }
    public DateTime? DonationDate { get; set; }
    public int? DoctorId { get; set; }
    public byte Process { get; set; } 

}

public class NoteUpdateDTO
{
    public string? Notes { get; set; }
}

public class AppointmentLastDonationDTO
{
    public bool HasDonationHistory { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public bool IsEditable { get; set; }
}

public class BloodDonationHistoryDTO
{
    public int DonationId { get; set; }
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public DateTime? DonationDate { get; set; }
    public string? BloodGroup { get; set; }
    public string? RhType { get; set; }
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}