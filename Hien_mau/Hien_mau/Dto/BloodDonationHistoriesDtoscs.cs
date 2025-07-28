namespace Hien_mau.DTOs;

public class BloodDonationHistoryCreateDTO
{
    public int AppointmentId { get; set; }
    public DateTime DonationDate { get; set; }
    public string? BloodGroup { get; set; } = null!;
    public string? RhType { get; set; } = null!;
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
}

public class BloodDonationHistoryUpdateDTO
{
    public int AppointmentId { get; set; }
    public DateTime DonationDate { get; set; }
    public string? BloodGroup { get; set; } = null!;
    public string? RhType { get; set; } = null!;
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
    public bool IsSuccess { get; set; }
}

public class BloodDonationHistoryDTO
{
    public int DonationId { get; set; }
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime DonationDate { get; set; }
    public string? BloodGroup { get; set; } = null!;
    public string? RhType { get; set; } = null!;
    public int? DoctorId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSuccess { get; set; }
}
public class BloodGroupUpdateDTO
{
    public string? BloodGroup { get; set; }
    public string? RhType { get; set; }
}
