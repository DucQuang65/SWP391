using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models
{
    public class BloodInventoryHistory
    {

        [Key]
        public int HistoryId { get; set; }
        public int? InventoryId { get; set; }
        public string? BloodGroup { get; set; }
        public string? RhType { get; set; }
        public string? ComponentType { get; set; }
        public string ActionType { get; set; }
        public int Quantity { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        [ForeignKey("PerformedByUser")]
        public int PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
        [ForeignKey("InventoryId")]
        public BloodInventory? Inventory { get; set; }
        public User PerformedByUser { get; set; }
    }
}