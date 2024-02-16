using AutoMapper;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.Registration
{
    /// <summary>
    /// Responsible for Registering a New User
    /// </summary>
    public class RegistrationBL : IRegistrationBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;

        public RegistrationBL(
            IUserBL userBL,
            IPasswordBL passwordBL)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
        }

        #region RegisterNewUserAsync
        /// <summary>
        /// Create a New User
        /// 
        /// Hash the User's Temp Password
        /// 
        /// Create the Password
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDto?> RegisterNewUserAsync(UserDto user)
        {
            try
            {
                if (user is null) return null;

                var dto = await _userBL.CreateAsync(user);

                if (!string.IsNullOrEmpty(user.TempPassword))
                    await _passwordBL.CreatePasswordForUserAsync(user.Id, user.TempPassword);

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
