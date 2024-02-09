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

        public async Task<PasswordDto> GeneratePasswordAsync(Guid userId, string password)
        {
            try
            {
                var dto = new PasswordDto();

                var (hash, salt) = _passwordHasher.HashPassword(password);

                dto.Salt = salt; dto.Hash = hash;

                if (userId != Guid.Empty)
                    dto.UserId = userId;

                var entity = _mapper.Map<Password>(dto);

                await _passwordRepository.CreateAsync(entity);

                dto = _mapper.Map<PasswordDto>(dto);

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PasswordVerificationResults> VerifyPassword(Guid userId, string providedPassword)
        {
            try
            {
                var entity = await _passwordRepository.GetByUserIdAsync(userId);

                if (entity == null) return PasswordVerificationResults.Failed;

                var dto = _mapper.Map<PasswordDto>(entity);

                var success = _passwordHasher.VerifyHashedPassword(dto, providedPassword);

                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
