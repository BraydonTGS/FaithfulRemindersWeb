using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Users
{
    /// <summary>
    /// User Business Logic
    /// </summary>
    public class UserBL : BaseBL<UserDto, User, Guid>, IUserBL
    {
        private readonly UserRepository _userRepository;

        public UserBL(UserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
    }
}
