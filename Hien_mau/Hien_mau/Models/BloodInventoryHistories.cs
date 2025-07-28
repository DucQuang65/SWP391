using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models;

public partial class BloodInventoryHistories
{
    [Key]
    public int HistoryId { get; set; } // Khóa chính

    public int InventoryId { get; set; } // FK → BloodInventory
    public BloodInventories Inventory { get; set; }

    public string ActionType { get; set; } // CheckIn, CheckOut, Hủy
    public int Quantity { get; set; } // Số lượng thay đổi (+/-)

    public string Notes { get; set; } // Ghi chú nếu có

    public int PerformedBy { get; set; } // FK → Users (ID người thực hiện)
    public Users PerformedByUser { get; set; }

    public DateTime PerformedAt { get; set; } // Ngày thực hiện thao tác

    public string BagType { get; set; } // Loại túi trong thao tác
    public DateTime? ReceivedDate { get; set; } // Ngày nhận (nếu là nhập kho)
    public DateTime? ExpirationDate { get; set; } // Ngày hết hạn (nếu là nhập kho)
}
