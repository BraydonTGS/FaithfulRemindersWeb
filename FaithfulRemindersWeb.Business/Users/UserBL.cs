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

        #region 

        #region CreateAsync - Override
        /// <summary>
        /// Creates a new DTO asynchronously.
        /// </summary>
        /// <param name="dto">The DTO to be created.</param>
        /// <returns>The created DTO after it is added to the database.</returns>
        public override async Task<UserDto?> CreateAsync(UserDto dto)
        {
            _log.Information($"Starting CreateAsync for the Specified User.");
            try
            {
                var entity = _mapper.Map<User>(dto);
                if (entity == null)
                {
                    _log.Warning($"Unable to Map DTO of Type UserDto to User.");
                    return null;
                }

                var createdEntity = await _userRepository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create Entity of Type User in Database.");
                    return null;
                }

                var resultDto = _mapper.Map<UserDto>(createdEntity);

                _log.Information($"Completed CreateAsync for UserDto. Entity Creation and Mapping Successful.");
                return resultDto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreateAsync for UserDto with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region GetUserByEmailAsync
        /// <summary>
        /// Get the User by the Specified Email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            _log.Information($"Starting VerifyUserPasswordAsync");
            try
            {
                var entity = await _userRepository.GetUserByEmailAsync(email);

                if (entity == null) 
                {
                    _log.Warning($"No Password Exists for the Specified User.");
                    return null;
                }

                var dto = _mapper.Map<UserDto>(entity);
                _log.Information($"Completed VerifyUserPasswordAsync. Successfully Verified the Specified Users Password");

                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in VerifyUserPasswordAsync with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
