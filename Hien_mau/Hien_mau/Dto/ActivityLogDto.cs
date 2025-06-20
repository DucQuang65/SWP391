using System.Collections.Generic;

namespace Hien_mau.Dto
{
    public class LogCreateDto
    {
        public int LogId { get; set; }
        public string? ActivityType { get; set; }
        public int? EntityId { get; set; }
        public string? EntityType { get; set; }
        public int UserID { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }

    public class LogUpdateDto
    {
        public int LogId { get; set; }
        public string? ActivityType { get; set; }
        public int? EntityId { get; set; }
        public string? EntityType { get; set; }
        public int UserID { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}