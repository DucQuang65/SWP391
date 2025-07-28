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
    public int InventoryId { get; set; } 

    public string BloodGroup { get; set; } 
    public string RhType { get; set; } 
    public string BagType { get; set; } 

    public int Quantity { get; set; } 
    public bool IsRare { get; set; } 
    public int Status { get; set; } 

    public DateTime LastUpdated { get; set; } 

    public int ComponentId { get; set; } 
    public virtual Components Components { get; set; } 

    public virtual ICollection<BloodInventoryHistories> BloodInventoryHistories { get; set; } = new List<BloodInventoryHistories>();
}
