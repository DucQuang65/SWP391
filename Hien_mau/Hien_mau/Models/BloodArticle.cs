using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class BloodArticle
{
    [Key]
    public int ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImgUrl { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
