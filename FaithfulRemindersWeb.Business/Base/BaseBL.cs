﻿using AutoMapper;
using FaithfulRemindersWeb.Entity.Entities.Base;
using Microsoft.Extensions.Logging;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Base Business Logic used for interacting with the <see cref="IBaseRepository{TEntity, TKey}"/>
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseBL<TDto, TEntity, TKey> : IBaseBL<TDto, TEntity, TKey>
        where TDto : BaseDto<TKey>
        where TEntity : BaseEntity<TKey>
    {
        IBaseRepository<TEntity, TKey> _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<BaseBL<TDto, TEntity, TKey>>();
            _mapper = mapper;
        }

        #region GetAllAsync
        /// <summary>
        /// Retrieves all DTOs of the specified type asynchronously.
        /// </summary>
        /// <returns>A collection of DTOs or null if none are found.</returns>
        public virtual async Task<IEnumerable<TDto>?> GetAllAsync()
        {
            try
            {
                _logger.BeginScope($"Begin Base Business Logic Get All Async.");

                var entities = await _repository.GetAllAsync();

                if (entities == null)
                {
                    _logger.LogWarning("No Entities Found.");
                    return null;
                }

                var results = _mapper.Map<IEnumerable<TDto>>(entities);

                _logger.LogDebug("Successfully Found {Count} Entities and Mapped to DTO of Type: {Type}", results.Count(), results.GetType());
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Getting All Async with Message");
                throw;
            }
        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Retrieves a DTO of the specified type by its unique identifier asynchronously.
        /// </summary>
        /// <param name="key">The unique identifier of the DTO to retrieve.</param>
        /// <returns>The retrieved DTO or null if not found.</returns>
        public virtual async Task<TDto?> GetByIdAsync(TKey key)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(key);

                if (entity == null) return null;

                var results = _mapper.Map<TDto>(entity);

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Creates a new DTO asynchronously.
        /// </summary>
        /// <param name="dto">The DTO to be created.</param>
        /// <returns>The created DTO after it is added to the database.</returns>
        public virtual async Task<TDto?> CreateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                if (entity == null) return null;

                var results = await _repository.CreateAsync(entity);

                if (results == null) return null;

                dto = _mapper.Map<TDto>(results);

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <returns>The updated DTO after changes are saved to the database.</returns>
        public virtual async Task<TDto?> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                if (entity is null) return null;

                var results = await _repository.UpdateAsync(entity);

                if (results == null) return null;

                dto = _mapper.Map<TDto>(results);

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// Soft deletes an DTO by setting an IsDeleted flag.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be soft-deleted.</param>
        /// <returns>True if the soft delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> SoftDeleteAsync(TKey key)
        {
            try
            {
                var results = await _repository.SoftDeleteAsync(key);

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// Hard deletes an entity from the database.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be hard-deleted.</param>
        /// <returns>True if the hard delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> HardDeleteAsync(TKey key)
        {
            try
            {
                var results = await _repository.HardDeleteAsync(key);

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region RestoreAsync
        /// <summary>
        /// Restores a soft-deleted DTO by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be restored.</param>
        /// <returns>True if the restoration is successful; otherwise, false.</returns>
        public virtual async Task<bool> RestoreAsync(TKey key)
        {
            try
            {
                var results = await _repository.RestoreAsync(key);

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
