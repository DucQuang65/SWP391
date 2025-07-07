using System.Collections.Generic;

namespace Hien_mau.Dto
{
    public class NotificationDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public int UserId { get; set; }
        public byte Priority { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.Now;
        
    }

    public class NotificationUpdateDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public int UserId { get; set; }
        public byte Priority { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.Now;
    }

}