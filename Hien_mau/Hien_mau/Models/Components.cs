using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Components
{
    [Key]
    public int ComponentId { get; set; }

    public string ComponentType { get; set; } = null!;

    public ICollection<BloodInventories> BloodInventories { get; set; }

    public virtual ICollection<BloodRequests> BloodRequests { get; set; } = new List<BloodRequests>();
}
