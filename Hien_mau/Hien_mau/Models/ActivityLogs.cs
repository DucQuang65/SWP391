using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models
{
    [Table("ActivityLogs")]
    public partial class ActivityLogs
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ActivityType { get; set; } = null!;

        public int? EntityId { get; set; }
        [Required]
        public string EntityType { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        [Required]
        public virtual Users User { get; set; } = null!;
    }
}
