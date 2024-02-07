using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Passwords
{
    internal class PasswordRepository : BaseRepository<Password, Guid>
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;
        public PasswordRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
