using FaithfulRemindersWeb.Entity.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FaithfulRemindersWeb.Entity.Entities
{
    /// <summary>
    /// Entity Object for the ToDo Item
    /// </summary>
    [Table("ToDoItem")]
    public class ToDoItem : BaseEntity<Guid>
    {

        [Required]
        [Column("Title")]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Column("DueDate")]
        public DateTime DueDate { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
