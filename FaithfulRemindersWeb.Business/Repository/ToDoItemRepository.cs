using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Repository.Base;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Repository
{
    /// <summary>
    /// ToDo Item Repository
    /// </summary>
    public class ToDoItemRepository : BaseRepository<ToDoItem, Guid>
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;

        public ToDoItemRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
