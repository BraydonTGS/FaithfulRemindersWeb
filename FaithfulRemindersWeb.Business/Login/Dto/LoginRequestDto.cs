using FaithfulRemindersWeb.Business.Base;

namespace FaithfulRemindersWeb.Business.Login.Dto
{
    public class LoginRequestDto : BaseDto<Guid>
    {
        public string UserName { get; set; } = string.Empty;
        public string TempPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
