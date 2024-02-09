using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Entity.Entities;
using System.Diagnostics;

namespace FaithfulRemindersWeb.Business.Passwords.Dto
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class PasswordDto : BaseDto<Guid>
    {
        public byte[] Hash { get; set; } = [];

        public byte[] Salt { get; set; } = [];

        public Guid UserId { get; set; }

        public User? User { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"Salt: {Salt} & Hash: {Hash}";
        }
    }
}
