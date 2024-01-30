using AutoMapper;
using FaithfulRemindersWeb.Entity.Entities.Base;
using Serilog;


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
        private readonly ILogger _log;
        private readonly IMapper _mapper;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            ILogger logger,
            IMapper mapper)
        {
            _repository = repository;
            _log = logger.ForContext<BaseBL<TDto, TEntity, TKey>>();
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
                _log.Information($"Begin Base Business Logic Get All Async.");

                _log.Information($"Get All Entities of Type: {typeof(TEntity).Name} Async");
                var entities = await _repository.GetAllAsync();

                if (entities == null)
                {
                    _log.Warning($"No Entities of Type: {typeof(TEntity).Name} Found.");
                    return null;
                }

                var results = _mapper.Map<IEnumerable<TDto>>(entities);

                _log.Information($"Successfully Found {results.Count()} Entities and Mapped to DTO of Type: {typeof(TDto)?.Name}");
                return results;

            }
            catch (Exception ex)
            {
                _log.Error($"Error Getting All Async with Message: {ex.Message}");
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
