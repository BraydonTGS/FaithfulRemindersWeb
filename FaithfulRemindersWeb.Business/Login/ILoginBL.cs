using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.Login
{
    public interface ILoginBL
    {
        Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto dto);
    }
}