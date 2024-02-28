using FaithfulRemindersWeb.Business.Base;

namespace FaithfulRemindersWeb.Business.Login.Dto
{
    /// <summary>
    /// Represents a Login Request Object when Logging in a Registered User
    /// </summary>
    public class LoginRequestDto : BaseDto<Guid>
    {
        public string UserName { get; set; } = string.Empty;
        public string TempPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
