using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using FaithfulRemindersWeb.Global.Constants;
using static FaithfulRemindersWeb.Global.Constants.Enums;

namespace FaithfulRemindersWeb.Business.Passwords
{
    public interface IPasswordBL : IBaseBL<PasswordDto, Password, Guid>
    {
        Task<PasswordDto> GeneratePasswordAsync(Guid userId, string password);
        Task<PasswordVerificationResults> VerifyPassword(Guid userId, string providedPassword);
    }
}
