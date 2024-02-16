using AutoMapper;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using Serilog.Sinks.File;

namespace FaithfulRemindersWeb.Business.Login
{
    public class LoginBL : ILoginBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;
        private readonly IMapper _mapper;

        public LoginBL(
            IUserBL userBL,
            IPasswordBL passwordBL,
            IMapper mapper)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto?> LoginUserAsync(UserDto dto)
        {
            try
            {
                if (dto is null) return null;

                var entity = await _userBL.GetUserByEmailAsync(dto.Email);

                if (entity is null) return null;

                var success = await _passwordBL.VerifyUserPasswordAsync(entity.Id, dto.TempPassword);

                if(success == Global.Constants.Enums.PasswordVerificationResults.Failed)
                {

                }
                dto = _mapper.Map<UserDto>(entity);

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
