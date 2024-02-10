using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        #region CreateAsync - Override
        public override async Task<Password?> CreateAsync(Password password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Check if a password for the user already exists
            bool passwordExists = await context.Passwords.AnyAsync(p => p.UserId == password.UserId);

            if (passwordExists)
            {
                throw new InvalidOperationException("A password for this user already exists.");
            }

            var newEntry = await context.Passwords.AddAsync(password);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion
    }
}
