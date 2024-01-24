using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Repository.Base;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : BaseRepository<User, Guid>
    {
        public UserRepository(IDbContextFactory<FaithfulDbContext> contextFactory) : base(contextFactory)
        {
        }
    }
}
