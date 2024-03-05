using FaithfulRemindersWeb.Entity.Entities.Base;
using FaithfulRemindersWeb.Global.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaithfulRemindersWeb.Entity.Entities
{
    [Table("UserRole")]
    public class UserRole : BaseEntity<Guid>
    {
        [Column("Role")]
        public Role Role { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
