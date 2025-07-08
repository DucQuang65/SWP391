namespace Hien_mau.Dtos
{
    public class CheckInOutRequestDto
    {
        public int InventoryID { get; set; }
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public int ComponentId { get; set; }
        public string BagType { get; set; } = null!; 
        public int Quantity { get; set; } 
        public string? Notes { get; set; }
        public int PerformedBy { get; set; }
    }

    public class BloodInventoryDto
    {
        public int InventoryID { get; set; }
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public int ComponentId { get; set; } 
        public string BagType { get; set; } = null!;
        public int Quantity { get; set; }
        public byte Status { get; set; }
        public bool IsRare { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? ExpirationDate { get; internal set; }
    }

    public class BloodHistoryDto
    {

        public int InventoryID { get; set; }
        public DateTime? PerformedAt { get; set; }
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public int ComponentId { get; set; } 
        public int Quantity { get; set; }
        public string PerformedByName { get; set; } = null!;
        public string ActionType { get; set; } = null!;
        public string? BagType { get; set; }
        public string? Notes { get; set; }
        public DateTime? ExpirationDate { get; internal set; }
    }
}