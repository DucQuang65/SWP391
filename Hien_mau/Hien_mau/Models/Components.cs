using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Components
{
    [Key]
    public int ComponentId { get; set; }

    public string ComponentType { get; set; } = null!;

   

    public virtual ICollection<BloodInventories> BloodInventories { get; set; } = new List<BloodInventories>();

    public virtual ICollection<BloodInventoryHistories> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistories>();

    public virtual ICollection<BloodRequests> BloodRequests { get; set; } = new List<BloodRequests>();
}
