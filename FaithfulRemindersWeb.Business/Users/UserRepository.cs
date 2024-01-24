using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Users
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository : BaseRepository<User, Guid>
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;

        public UserRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
