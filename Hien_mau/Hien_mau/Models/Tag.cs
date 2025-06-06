using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Tag
{
    [Key]
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<BloodArticle> Articles { get; set; } = new List<BloodArticle>();

    public virtual ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
}
