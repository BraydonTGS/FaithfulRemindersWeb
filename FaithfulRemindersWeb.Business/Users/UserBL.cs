using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Serilog;

namespace FaithfulRemindersWeb.Business.Users
{
    /// <summary>
    /// User Business Logic
    /// Responsible for Repository Interaction
    /// </summary>
    public class UserBL : BaseBL<UserDto, User, Guid>, IUserBL
    {
        private readonly UserRepository _userRepository;
        public UserBL(
            UserRepository userRepository,
            ILogger logger,
            IMapper mapper) : base(userRepository, logger, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        #region GetUserByEmailAsync
        /// <summary>
        /// Get the User by the Specified Email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var entity = await _userRepository.GetUserByEmailAsync(email);

            if (entity == null) { }

            var dto = _mapper.Map<UserDto>(entity);

            return dto;
        }
        #endregion
    }
}
