using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodInventories
{
    [Key]
    public int InventoryId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public int ComponentId { get; set; }

    public int Quantity { get; set; }

    public bool? IsRare { get; set; }

    public byte Status { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? BagType { get; set; }

    public DateTime ReceivedDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual ICollection<BloodInventoryHistories> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistories>();

    public virtual Components Component { get; set; } = null!;
}
