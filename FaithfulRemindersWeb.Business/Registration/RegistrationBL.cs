using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using Serilog;

namespace FaithfulRemindersWeb.Business.Registration
{
    /// <summary>
    /// Responsible for Registering a New User
    /// </summary>
    public class RegistrationBL : IRegistrationBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;
        private readonly ILogger _log;

        public RegistrationBL(
            IUserBL userBL,
            IPasswordBL passwordBL,
            ILogger logger)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
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
            _log.Information($"Starting RegisterNewUserAsync for the New User.");
            try
            {
                if (user is null) return null;

                var dto = await _userBL.CreateAsync(user);

                if (!string.IsNullOrEmpty(user.TempPassword))
                    await _passwordBL.CreatePasswordForUserAsync(user.Id, user.TempPassword);

                _log.Information($"Successfully Registered the New User!");
                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in RegisterNewUserAsync for with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

    }
}
