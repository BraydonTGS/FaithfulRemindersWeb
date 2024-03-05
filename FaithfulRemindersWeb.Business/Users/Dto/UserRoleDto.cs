using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Global.Enums;

namespace FaithfulRemindersWeb.Business.Users.Dto
{
    /// <summary>
    /// User's Role Data Transfer Object
    /// </summary>
    public class UserRoleDto : BaseDto<Guid>
    {
        public Role Role { get; set; }

        public Guid UserId { get; set; }

        public UserDto? User { get; set; }
    }
}
