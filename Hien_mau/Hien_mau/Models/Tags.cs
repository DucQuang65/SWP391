using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class Tags
{
    [Key]
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<BloodArticles> Articles { get; set; } = new List<BloodArticles>();

    public virtual ICollection<News> Posts { get; set; } = new List<News>();
}
