using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaithfulRemindersWeb.Entity.Entities.Base
{
    /// <summary>
    /// Base Entity Object for all Entities
    /// </summary>
    public abstract class BaseEntity<TKey>
    {
        [Key]
        [Required]
        [Column("Id")]
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDirty { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

    }
}
