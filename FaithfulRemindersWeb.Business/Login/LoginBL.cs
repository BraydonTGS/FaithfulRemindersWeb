using AutoMapper;
using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Global.Enums;
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
        public async Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto request)
        {
            _log.Information($"Starting LoginUserAsync for the Specified User.");
            try
            {
                if (request is null) return null;

                var dto = _mapper.Map<UserDto>(request);

                var entity = await _userBL.GetUserByEmailAsync(dto.Email);

                if (entity is null)
                {
                    _log.Warning($"No User Found for with the Specified Email");
                    return null;
                }

                var success = await _passwordBL.VerifyUserPasswordAsync(entity.Id, dto.TempPassword);

                if (success == PasswordVerificationResults.Failed)
                {
                    _log.Warning($"Password Verification Failure for the Specified User with the Email: {dto.Email}");
                    throw new InvalidPasswordException($"Password Verification Failure for the Specified User");
                }

                dto = _mapper.Map<UserDto>(entity);

                if (dto is null)
                {
                    _log.Warning($"Error Generating UserDto from the User Entity");
                    return null;
                }

                var response = _mapper.Map<LoginResponseDto>(dto);

                if (response is not null)
                    response.AccessToken = "I NEED TO GENERATE AN ACCESS TOLKEN";

                _log.Information($"Completed LoginUserAsync. Successfully Verified and Mapped the Specified User with the Email: {dto.Email}.");
                return response;
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
