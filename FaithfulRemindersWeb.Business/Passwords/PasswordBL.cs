using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Helpers;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Serilog;


namespace FaithfulRemindersWeb.Business.Passwords
{
    internal class PasswordBL : BaseBL<PasswordDto, Password, Guid>, IPasswordBL
    {
        private readonly PasswordRepository _passwordRepository;
        private readonly IHasher _hasher;

        public PasswordBL(
            PasswordRepository passwordRepository,
            IHasher passwordGenerator,
            ILogger logger,
            IMapper mapper) : base(passwordRepository, logger, mapper)
        {
            _passwordRepository = passwordRepository ?? throw new ArgumentNullException(nameof(passwordRepository));
            _hasher = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
        }

        public async Task<PasswordDto> GeneratePasswordAsync(Guid userId, string password)
        {
            try
            {
                var dto = new PasswordDto();

                var (salt, hash) = _hasher.GenerateHash(password);

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
    }
}
