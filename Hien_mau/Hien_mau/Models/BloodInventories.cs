using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Hien_mau.Models;

public partial class BloodInventories
{
    [Key]
    [JsonIgnore]
    public int InventoryId { get; set; } // Khóa chính

    public string BloodGroup { get; set; } // Nhóm máu (A, B, AB, O)
    public string RhType { get; set; } // Rh+ hoặc Rh-
    public string BagType { get; set; } // Loại túi: 250ml, 350ml, 450ml

    public int Quantity { get; set; } // Số lượng hiện có
    public bool IsRare { get; set; } // Có phải máu hiếm không (Rh-)
    public int Status { get; set; } // 0=Khẩn cấp, 1=Thiếu, 2=Trung bình, 3=An toàn

    public DateTime LastUpdated { get; set; } // Ngày cập nhật gần nhất

    public int ComponentId { get; set; } // Thành phần máu (Hồng cầu, Tiểu cầu, Huyết tương,...)
    public virtual Component Component { get; set; } 

    public virtual ICollection<BloodInventoryHistories> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistories>();

    
}
