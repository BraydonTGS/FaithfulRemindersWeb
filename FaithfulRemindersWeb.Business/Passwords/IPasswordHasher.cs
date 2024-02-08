using static FaithfulRemindersWeb.Global.Constants.Enums;

namespace FaithfulRemindersWeb.Business.Passwords
{
    /// <summary>
    /// Interface used to define methods needed for Hashing and Salting
    /// </summary>
    public interface IPasswordHasher<TPassword> where TPassword : class
    {
        public (byte[] hash, byte[] salt) HashPassword(string password);

        public PasswordVerificationResults VerifyHashedPassword(TPassword password, string providedPassword);
    }
}
