using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Roles
{
    [Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
