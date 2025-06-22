namespace Hien_mau.Dtos
{
    public class CheckInOutRequestDto
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public int PerformedBy { get; set; }
    }
}