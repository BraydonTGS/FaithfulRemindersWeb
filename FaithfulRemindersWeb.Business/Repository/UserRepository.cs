using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Repository.Base;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Repository
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
