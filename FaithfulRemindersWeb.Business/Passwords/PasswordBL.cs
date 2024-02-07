using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Serilog;


namespace FaithfulRemindersWeb.Business.Passwords
{
    internal class PasswordBL : BaseBL<PasswordDto, Password, Guid>, IPasswordBL
    {
        private readonly PasswordRepository _passwordRepository;
        private readonly IPasswordGenerator _passwordGenerator;

        public PasswordBL(
            PasswordRepository passwordRepository,
            IPasswordGenerator passwordGenerator,
            ILogger logger,
            IMapper mapper) : base(passwordRepository, logger, mapper)
        {
            _passwordRepository = passwordRepository ?? throw new ArgumentNullException(nameof(passwordRepository));
            _passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
        }

        public async Task<PasswordDto> GeneratePasswordAsync(string password)
        {
            try
            {
                var dto = _passwordGenerator.GeneratePassword(password);

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
