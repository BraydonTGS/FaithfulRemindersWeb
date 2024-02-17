using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using FaithfulRemindersWeb.Global.Exceptions;
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

        #region CreateAsync - Override
        /// <summary>
        /// When Creating a new Password in the Database, ensure one does not already exist for the Given User.
        /// 
        /// If so, Throw a PasswordAlreadyExistsException
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="PasswordAlreadyExistsException"></exception>
        public override async Task<Password?> CreateAsync(Password password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Check if a password for the user already exists
            bool passwordExists = await context.Passwords.AnyAsync(p => p.UserId == password.UserId);

            if (passwordExists)
                throw new PasswordAlreadyExistsException("A password for this user already exists.");

            var newEntry = await context.Passwords.AddAsync(password);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion
    }
}
