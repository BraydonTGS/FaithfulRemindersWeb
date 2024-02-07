using FaithfulRemindersWeb.Entity.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FaithfulRemindersWeb.Entity.Entities
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Password : BaseEntity<Guid>
    {
        [Required]
        [Column("Hash")]
        [MaxLength(100)]
        public byte[] Hash { get; set; } = Array.Empty<byte>();

        [Required]
        [Column("Salt")]
        public byte[] Salt { get; set; } = Array.Empty<byte>();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"Salt: {Salt} & Hash: {Hash}";
        }
    }
}
