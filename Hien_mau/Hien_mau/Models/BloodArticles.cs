using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodArticles
{
    [Key]
    public int ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImgUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual Users User { get; set; } = null!;

    public virtual ICollection<Tags> Tags { get; set; } = new List<Tags>();
}
