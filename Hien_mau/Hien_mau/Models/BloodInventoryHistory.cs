using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models
{
    public class BloodInventoryHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [Required]
        public int InventoryID { get; set; }

        [Required]
        public string BloodGroup { get; set; } = null!;

        [Required]
        public string RhType { get; set; } = null!;

        [Required]
        public string ComponentType { get; set; } = null!;

        [Required]
        public string ActionType { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }


        [Required]
        public int PerformedBy { get; set; }

        [Required]
        public DateTime PerformedAt { get; set; }
        public string Notes { get; set; }

        public string? BagType { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [ForeignKey("InventoryID")]
        public BloodInventory BloodInventory { get; set; } = null!;

        [ForeignKey("PerformedBy")]
        public User PerformedByUser { get; set; } = null!;
    }
}