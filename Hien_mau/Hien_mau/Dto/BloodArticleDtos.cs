using System.Collections.Generic;

namespace Hien_mau.Dto
{
    public class BloodArticleCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public List<int>? TagIds { get; set; }
    }

    public class BloodArticleUpdateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public List<int>? TagIds { get; set; }
    }
}