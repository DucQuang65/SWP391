using System;
using System.ComponentModel.DataAnnotations;

namespace Hien_mau.Models
{
    public partial class ActivityLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; } = null!;

        public int? EntityId { get; set; }

        [Required]
        [StringLength(20)]
        public string EntityType { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public virtual User User { get; set; } = null!;
    }
}
