using FaithfulRemindersWeb.Business.Passwords.Dto;

namespace FaithfulRemindersWeb.Business.Passwords
{
    public interface IPasswordGenerator
    {
        public PasswordDto GeneratePassword(string password);
    }
}
