using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Serilog;
using static FaithfulRemindersWeb.Global.Constants.Enums;


namespace FaithfulRemindersWeb.Business.Passwords
{
    public class PasswordBL : BaseBL<PasswordDto, Password, Guid>, IPasswordBL
    {
        private readonly PasswordRepository _passwordRepository;
        private readonly IPasswordHasher<PasswordDto> _passwordHasher;

        public PasswordBL(
            PasswordRepository passwordRepository,
            IPasswordHasher<PasswordDto> passwordHasher,
            ILogger logger,
            IMapper mapper) : base(passwordRepository, logger, mapper)
        {
            _passwordRepository = passwordRepository ?? throw new ArgumentNullException(nameof(passwordRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        #region CreatePasswordForUserAsync
        /// <summary>
        /// Create a Password Entity for the Specified User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<PasswordDto?> CreatePasswordForUserAsync(Guid userId, string password)
        {
            _log.Information($"Starting CreatePasswordForUserAsync");
            try
            {
                var dto = new PasswordDto();

                var (hash, salt) = _passwordHasher.HashPassword(password);            

                dto.Salt = salt; dto.Hash = hash;
               
                if (userId != Guid.Empty)
                    dto.UserId = userId;

                var entity = _mapper.Map<Password>(dto);

                entity = await _passwordRepository.CreateAsync(entity);

                if(entity is null) 
                {
                    _log.Error("Failed to Create Password Entity with the Specified UserId");
                    return null;
                }

                dto = _mapper.Map<PasswordDto>(entity);

                _log.Information($"Completed CreatePasswordForUserAsync. Hashed, Created and Mapped the User's Password Successfully");
                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreatePasswordForUserAsync with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region VerifyUserPasswordAsync
        /// <summary>
        /// Verify the Specified User's Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public async Task<PasswordVerificationResults> VerifyUserPasswordAsync(Guid userId, string providedPassword)
        {
            _log.Information($"Starting VerifyUserPasswordAsync");
            try
            {
                var entity = await _passwordRepository.GetByUserIdAsync(userId);

                if (entity == null)
                {
                    _log.Warning($"No Password Exists for the Specified User.");
                    return PasswordVerificationResults.Failed;
                }

                var dto = _mapper.Map<PasswordDto>(entity);

                var results = _passwordHasher.VerifyHashedPassword(dto, providedPassword);

                _log.Information($"Completed VerifyUserPasswordAsync with Results: {results}");
                return results;
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
