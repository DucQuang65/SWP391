using System.Collections.Generic;

namespace Hien_mau.Dto
{
    public class BlogPostCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public int UserId { get; set; }
        public byte? Status { get; set; } // Sửa từ byte? thành int?
        public List<int>? TagIds { get; set; }
    }

    public class BlogPostUpdateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public byte? Status { get; set; } // Sửa từ byte? thành int?
        public List<int>? TagIds { get; set; }
    }
}