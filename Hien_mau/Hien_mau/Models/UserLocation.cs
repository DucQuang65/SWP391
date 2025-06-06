using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class UserLocation
{
    [Key]
    public int LocationId { get; set; }

    public int UserId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
