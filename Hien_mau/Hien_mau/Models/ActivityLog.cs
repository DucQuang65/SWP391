using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_mau.Models
{
    [Table("ActivityLog")]
    public partial class ActivityLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string ActivityType { get; set; } = null!;

        public int? EntityId { get; set; }

        [Required]
        public string EntityType { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        [Required]
        public virtual User User { get; set; } = null!;

    }
}