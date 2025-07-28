using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class RequestComponent
{
    [Key]
    public int ComponentId { get; set; }

    public int RequestId { get; set; }

    public string ComponentType { get; set; } = null!;

    public virtual BloodRequests Request { get; set; } = null!;
}
