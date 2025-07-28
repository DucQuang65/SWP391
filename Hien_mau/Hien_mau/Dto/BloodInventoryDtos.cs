namespace Hien_mau.Dtos
{
    public static class BloodInventoryDtos
    {
        public class BloodInventoryDto
        {
            public int InventoryID { get; set; } // ID kho máu
            public string BloodGroup { get; set; } // Nhóm máu
            public string RhType { get; set; } // Loại Rh
            public int ComponentId { get; set; } // ID thành phần máu
            public string BagType { get; set; } // Loại túi máu
            public int Quantity { get; set; } // Số lượng
            public byte Status { get; set; } // Trạng thái: 0=Khẩn cấp, 1=Thiếu, 2=Trung bình, 3=An toàn
            public bool IsRare { get; set; } // Máu hiếm
            public DateTime LastUpdated { get; set; } // Ngày cập nhật
        }

        public class BloodHistoryDto
        {
            public int InventoryID { get; set; } // ID kho máu
            public DateTime PerformedAt { get; set; } // Thời điểm thực hiện
            public string BloodGroup { get; set; } // Nhóm máu
            public string RhType { get; set; } // Loại Rh
           
            public int Quantity { get; set; } 
          

            public string PerformedByName { get; set; } // Tên người thực hiện
            public string ActionType { get; set; } // Hành động
            public string BagType { get; set; } // Loại túi máu
            public string Notes { get; set; } // Ghi chú
            public DateTime? ReceivedDate { get; set; } // Ngày nhận
            public DateTime? ExpirationDate { get; set; } // Ngày hết hạn
        }

        public class ComponentSelectionDto
        {
            public int ComponentId { get; set; } // ID thành phần máu
            public string ComponentName { get; set; } // Tên thành phần máu
        }

        public class BloodSelectionDto
        {
            public List<ComponentSelectionDto> Components { get; set; } // Danh sách thành phần máu
            public List<string> BloodGroups { get; set; } // Danh sách nhóm máu
            public List<string> RhTypes { get; set; } // Danh sách loại Rh
        }

        public class CheckInOutRequestDto
        {
            public string BloodGroup { get; set; } // Nhóm máu
            public string RhType { get; set; } // Loại Rh
            public int ComponentId { get; set; } // ID thành phần máu
            public int Quantity { get; set; } // Số lượng
            public string BagType { get; set; } // Loại túi máu
            public string Notes { get; set; } // Ghi chú
            public int PerformedBy { get; set; } // ID người thực hiện
        }

        public class BloodInventoryStatisticsDto
        {
            public string? BloodGroup { get; set; }
            public string? RhType { get; set; }
            public int ComponentId { get; set; }
            public string? ComponentName { get; set; }
            public int Quantity { get; set; }
        }

    }
}