using AutoMapper;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using Serilog;

namespace FaithfulRemindersWeb.Business.Registration
{
    public class RegistrationBL : IRegistrationBL
    {
        private readonly UserBL _userBL;
        private readonly PasswordBL _passwordBL;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RegistrationBL(
            UserBL userBL,
            PasswordBL passwordBL,
            ILogger logger,
            IMapper mapper)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));

            _mapper = mapper;
            _logger = logger;
        }

#region RegisterNewUserAsync
        public async Task<UserDto?> RegisterNewUserAsync(UserDto user)
        {
            if(user is null) return null;

            var dto = await _userBL.CreateAsync(user);

            if(!string.IsNullOrEmpty(user.Password))
                await _passwordBL.CreatePasswordForUserAsync(user.Id, user.Password);

            return dto;

        }
        #endregion

    }
}
