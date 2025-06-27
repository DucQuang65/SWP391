namespace Hien_mau.Dtos
{
    public class CheckInOutRequestDto
    {
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public string ComponentType { get; set; } = null!;
        public string BagType { get; set; } = null!; // 250ml, 350ml, 450ml
        public int Quantity { get; set; } // túi
        public string? Notes { get; set; }
        public int PerformedBy { get; set; }
    }

    public class BloodInventoryDto
    {
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public string ComponentType { get; set; } = null!;
        public string BagType { get; set; } = null!;
        public int Quantity { get; set; }
        public byte Status { get; set; }
        public bool IsRare { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? ExpirationDate { get; internal set; }
    }

    public class BloodHistoryDto
    {
        public DateTime PerformedAt { get; set; }
        public string BloodGroup { get; set; } = null!;
        public string RhType { get; set; } = null!;
        public string ComponentType { get; set; } = null!;
        public int Quantity { get; set; }
        public string PerformedByName { get; set; } = null!;
        public string ActionType { get; set; } = null!;
        public string? BagType { get; set; }
        public string? Notes { get; set; }
        public DateTime? ExpirationDate { get; internal set; }
    }
}