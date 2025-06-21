using System.Collections.Generic;

namespace Hien_mau.Dto
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public int UserId { get; set; }
        public List<int>? TagIds { get; set; }
    }

    public class NewsUpdateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public int UserId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}