using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models
{
    public class Reminder
    {
        [Key]
        public int ReminderId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Type { get; set; }           
        public string Message { get; set; }
        public DateTime RemindAt { get; set; }
        public bool IsDisabled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsSent { get; set; } = false;
        public DateTime? SentAt { get; set; }

      
        public virtual Users User { get; set; }
    }
}
