using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using FaithfulRemindersWeb.Global.Enums;

namespace FaithfulRemindersWeb.Business.Passwords
{
    public interface IPasswordBL : IBaseBL<PasswordDto, Password, Guid>
    {
        Task<PasswordDto?> CreatePasswordForUserAsync(Guid userId, string password);
        Task<PasswordVerificationResults> VerifyUserPasswordAsync(Guid userId, string providedPassword);
    }
}
