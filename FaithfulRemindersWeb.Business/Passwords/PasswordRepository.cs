using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Passwords
{
    public class PasswordRepository : BaseRepository<Password, Guid>
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;
        public PasswordRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        #region GetByUserIdAsync
        /// <summary>
        /// Get the Password Entity by the Specified UserId
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Password?> GetByUserIdAsync(Guid key)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Set<Password>()
                .Where(x => x.UserId == key)
                .FirstOrDefaultAsync();

            return result;
        }
        #endregion
    }
}
