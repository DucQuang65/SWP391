using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class HospitalInfo
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? WorkingHours { get; set; }

    public string? MapImageUrl { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
