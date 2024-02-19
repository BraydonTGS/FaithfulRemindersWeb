using AutoMapper;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Global.Exceptions;
using Serilog;

namespace FaithfulRemindersWeb.Business.Login
{
    public class LoginBL : ILoginBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;
        private readonly IMapper _mapper;
        private readonly ILogger _log;

        public LoginBL(
            IUserBL userBL,
            IPasswordBL passwordBL,
            ILogger logger,
            IMapper mapper)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region LoginUserAsync
        /// <summary>
        /// Attempt to Login the Specified User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<UserDto?> LoginUserAsync(UserDto dto)
        {
            _log.Information($"Starting LoginUserAsync for the Specified User.");
            try
            {
                if (dto is null) return null;

                var entity = await _userBL.GetUserByEmailAsync(dto.Email);

                if (entity is null)
                {
                    _log.Warning($"No User Found for with the Specified Email");
                    return null;
                }

                var success = await _passwordBL.VerifyUserPasswordAsync(entity.Id, dto.TempPassword);

                if (success == Global.Constants.Enums.PasswordVerificationResults.Failed)
                {
                    _log.Warning($"Password Verification Failure for the Specified User with the Email: {dto.Email}");
                    throw new InvalidPasswordException($"Password Verification Failure for the Specified User");
                }

                dto = _mapper.Map<UserDto>(entity);

                _log.Information($"Completed LoginUserAsync. Successfully Verified and Mapped the Specified User with the Email: {dto.Email}.");
                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in LoginUserAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
