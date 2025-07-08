using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models;

public partial class News
{
    [Key]
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImgUrl { get; set; }

    public int UserId { get; set; }

    public DateTime? PostedAt { get; set; }

    public virtual Users User { get; set; } = null!;

    public virtual ICollection<Tags> Tags { get; set; } = new List<Tags>();
}
