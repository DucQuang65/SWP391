namespace Hien_mau.Models;

public class Appointments
{
    public int AppointmentID { get; set; }
    public int UserID { get; set; }
    public int? DoctorID1 { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string TimeSlot { get; set; }
    public bool? Status { get; set; }
    public byte Process { get; set; } = 0;
    public bool Cancel { get; set; } = false;
    public string? Notes { get; set; }
    public string? BloodPressure { get; set; }
    public int? HeartRate { get; set; }
    public double? Hemoglobin { get; set; }
    public double? Temperature { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public double? WeightAppointment { get; set; }
    public double? HeightAppointment { get; set; }
    public double? DonationCapacity { get; set; }
    public DateTime? DonationDate { get; set; }
    public string? BloodGroup { get; set; }
    public string? RhType { get; set; }
    public int? DoctorID2 { get; set; }

    // Navigation properties (nếu cần)
    public Users User { get; set; }
    public Users Doctor1 { get; set; }
    public Users Doctor2 { get; set; }
}