using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class BlogPost
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImgUrl { get; set; }

    public int UserId { get; set; }

    public DateTime? PostedAt { get; set; }

    public byte? Status { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
