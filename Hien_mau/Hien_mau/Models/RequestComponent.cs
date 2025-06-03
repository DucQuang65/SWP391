using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class RequestComponent
{
    public int ComponentId { get; set; }

    public int RequestId { get; set; }

    public string ComponentType { get; set; } = null!;

    public virtual BloodRequest Request { get; set; } = null!;
}
