using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Passwords
{
    internal interface IPasswordBL : IBaseBL<PasswordDto, Password, Guid>
    {
    }
}
