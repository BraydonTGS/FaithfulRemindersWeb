﻿
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Interface for defining basic CRUD operations for DTOs
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseBL<TDto, TEntity, TKey> where TDto : BaseDto<TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TDto>?> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey key);
        Task<TDto?> CreateAsync(TDto dto);
        Task<TDto?> UpdateAsync(TDto dto);
        Task<bool> SoftDeleteAsync(TKey key);
        Task<bool> HardDeleteAsync(TKey key);
        Task<bool> RestoreAsync(TKey key);
    }
}
