using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Dto;

public class BloodDonationSubmissionDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime RequestedDonationDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string TimeSlot { get; set; }

    [Required]
    public float Weight { get; set; }

    [Required]
    public float Height { get; set; }

    [Required]
    public bool HasDonated { get; set; }
}