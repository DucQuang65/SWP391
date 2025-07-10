using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models;

public partial class BloodInventoryHistories
{
    [Key]
    public int HistoryId { get; set; }

    public int? InventoryId { get; set; }
    [Required]
    public string? BloodGroup { get; set; }
    [Required]
    public string? RhType { get; set; }
    [Required]
    public int ComponentId { get; set; }
    [Required]
    public string ActionType { get; set; } = null!;
    [Required]
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    [Required]
    public int PerformedBy { get; set; }
    [Required]
    public DateTime? PerformedAt { get; set; }

    public string? BagType { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual Components Component { get; set; } = null!;

    public virtual BloodInventories? Inventory { get; set; }

    public virtual Users PerformedByNavigation { get; set; } = null!;

    [ForeignKey("InventoryId")]
    public BloodInventories BloodInventory { get; set; } = null!;

    [ForeignKey("PerformedBy")]
    public Users PerformedByUser { get; set; } = null!;
}
