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

        public PasswordBL(
            PasswordRepository passwordRepository,
            ILogger logger,
            IMapper mapper) : base(passwordRepository, logger, mapper)
        {
            _passwordRepository = passwordRepository ?? throw new ArgumentNullException(nameof(passwordRepository));
        }
    }
}
