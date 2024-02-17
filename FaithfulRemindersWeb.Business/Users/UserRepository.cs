using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using FaithfulRemindersWeb.Global.Exceptions;
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
        /// Get the User by Email - There can only be One Unique Email in the Database
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

        #region CreateAsync - Override
        /// <summary>
        /// When Creating a New User, Ensure that the Email Does not already Exist in the Database
        /// 
        /// If so, Throw an EmailAlreadyRegisteredException
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override async Task<User?> CreateAsync(User user)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Check if an email for the user already exists
            bool emailIsRegistered = await context.Set<User>().AnyAsync(x => x.Email == user.Email);

            if (emailIsRegistered)
                throw new EmailAlreadyRegisteredException($"The Specified Email is already Registered");

            var newEntry = await context.Set<User>().AddAsync(user);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion

    }
}
