using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.Login.Dto
{
    /// <summary>
    /// Represents a LoginResponse Object when Logging in a Registered User
    /// </summary>
    public class LoginResponseDto : BaseDto<Guid>
    {
        public string AccessToken { get; set; } = string.Empty;
        public UserDto? User { get; set; }
    }
}
