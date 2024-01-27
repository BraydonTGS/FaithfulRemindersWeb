using FaithfulRemindersWeb.Entity.Entities.Base;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FaithfulRemindersWeb.Entity.Entities
{
    /// <summary>
    /// Entity Object for the Application User
    /// </summary>
    [Table("Users")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class User : BaseEntity<Guid>
    {

        [Required]
        [Column("FirstName")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("LastName")]
        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("Email")]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("UserName")]
        [MaxLength(25)]
        public string UserName { get; set; } = string.Empty;

        public ICollection<ToDoItem>? ToDoListItems { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}";
        }
    }
}
