using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.Extensions.Logging;

namespace FaithfulRemindersWeb.Business.Users
{
    /// <summary>
    /// User Business Logic
    /// Responsible for Repository Interaction
    /// </summary>
    public class UserBL : BaseBL<UserDto, User, Guid>, IUserBL
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger _logger;   
        private readonly IMapper _mapper;

        public UserBL(
            UserRepository userRepository, 
            ILoggerFactory loggerFactory, 
            IMapper mapper) : base(userRepository, loggerFactory, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = loggerFactory.CreateLogger<UserBL>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
    }
}
