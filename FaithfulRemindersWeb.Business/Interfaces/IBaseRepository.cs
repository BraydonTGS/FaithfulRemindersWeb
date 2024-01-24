using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Interfaces
{
    /// <summary>
    /// Generic interface defining basic CRUD (Create, Read, Update, Delete) operations for entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    internal interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>?> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task<TEntity?> CreateAsync(TEntity entity);
        public Task<TEntity?> UpdateAsync(TEntity entity);
        public Task<bool> SoftDeleteAsync(TKey id);
        public Task<bool> RestoreAsync(TKey id);
        public Task<bool> HardDeleteAsync(TKey id);
    }
}
