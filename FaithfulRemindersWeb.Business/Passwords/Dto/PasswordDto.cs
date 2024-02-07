using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Entity.Entities;
using System.Diagnostics;

namespace FaithfulRemindersWeb.Business.Passwords.Dto
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    internal class PasswordDto : BaseDto<Guid>
    {
        public byte[] Hash { get; set; } = Array.Empty<byte>();

        public byte[] Salt { get; set; } = Array.Empty<byte>();

        public Guid UserId { get; set; }

        public User? User { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"Salt: {Salt} & Hash: {Hash}";
        }
    }
}
