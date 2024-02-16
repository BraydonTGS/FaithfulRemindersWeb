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

        #region GetUserByEmailAsync
        /// <summary>
        /// The the User by Email - There can only be One Unique Email in the Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Set<User>()
                .FirstOrDefaultAsync(x => x.Email == email);

            return result;
        }
        #endregion

    }
}
