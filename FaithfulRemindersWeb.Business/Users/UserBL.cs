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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserBL(
            UserRepository userRepository,
            ILogger logger,
            IMapper mapper) : base(userRepository, logger, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger.ForContext<UserBL>() ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
    }
}
