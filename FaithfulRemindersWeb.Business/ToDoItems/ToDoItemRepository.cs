using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.ToDoItems
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


        #region GetToDoItemByIdIncludeUserAsync
        /// <summary>
        /// Get a ToDo Item with the specified Id and Include the associated User. 
        /// 
        /// Do Not Include any Items that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="toDoItemId"></param>
        /// <returns>A Single ToDoItem including the User</returns>
        public async Task<ToDoItem> GetToDoItemByIdIncludeUserAsync(Guid toDoItemId)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await context.Set<ToDoItem>()
                .Where(x => x.Id == toDoItemId && !x.IsDeleted)
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            return result;
        }
        #endregion

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// Get all of the ToDo Items for the Specified User where the ToDo Item UserId is equal to the Id of the Given User. 
        /// 
        /// Do Not Include any Items that are soft deleted. 
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A Collection of ToDoItems for the Specified User</returns>
        public async Task<IEnumerable<ToDoItem>> GetAllToDoItemsByUserIdAsync(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var results = await context.Set<ToDoItem>()
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

            return results;
        }
        #endregion

        #region GetAllSoftDeletedToDoItemsByUserIdAsync
        /// <summary>
        /// Get all of the ToDo Items for the Specified User where the ToDo Item UserId is equal to the Id of the Given User.
        /// 
        /// Where the ToDo Items are Soft Deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A Collection of ToDoItems for the Specified User</returns>
        public async Task<IEnumerable<ToDoItem>> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var results = await context.Set<ToDoItem>()
                .Where(x => x.UserId == userId && x.IsDeleted)
                .ToListAsync();

            return results;
        }
        #endregion
    }
}
