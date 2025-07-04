﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodInventory

{
    [Key]
    public int InventoryID { get; set; }

    public string BloodGroup { get; set; } = null!;

    public string RhType { get; set; } = null!;
    public string BagType { get; set; } = null!;
    public string ComponentType { get; set; } = null!;

    public int Quantity { get; set; }

    public bool? IsRare { get; set; }
    public byte Status { get; set; }
    public DateTime ReceivedDate { get; set; }

    public DateTime? LastUpdated { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public ICollection<BloodInventoryHistory> Histories { get; set; } = new List<BloodInventoryHistory>();
}
