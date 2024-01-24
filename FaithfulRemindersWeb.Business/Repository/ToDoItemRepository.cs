using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Repository.Base;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Repository
{
    /// <summary>
    /// ToDo Item Repository
    /// Add any Repository Logic Specific for a ToDo Item
    /// that is not implemented by the <see cref="BaseRepository{TEntity, TKey}"/>
    /// </summary>
    public class ToDoItemRepository : BaseRepository<ToDoItem, Guid>
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;

        public ToDoItemRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        #region GetAllByUserIdAsync
        /// <summary>
        /// Get all of the ToDo Items for the Specified User 
        /// where the ToDo Item UserId is equal to the Id of the Given User. 
        /// 
        /// Do Not Include any Items that are soft deleted. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Collection of ToDoItems for the Specified User</returns>
        public async Task<List<ToDoItem>> GetAllToDoItemsByUserIdAsync(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var results = await context.Set<ToDoItem>()
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

            return results;
        }
        #endregion
    }
}
