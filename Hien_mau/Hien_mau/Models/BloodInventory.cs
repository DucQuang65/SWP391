using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class BloodInventory
{
    public int InventoryId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;

    public string ComponentType { get; set; } = null!;

    public int Quantity { get; set; }

    public bool? IsRare { get; set; }

    public DateTime? LastUpdated { get; set; }
}
