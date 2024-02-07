using FaithfulRemindersWeb.Business.Passwords.Dto;

namespace FaithfulRemindersWeb.Business.Passwords
{
    internal interface IPasswordGenerator
    {
        public PasswordDto GeneratePassword(string password);
    }
}
