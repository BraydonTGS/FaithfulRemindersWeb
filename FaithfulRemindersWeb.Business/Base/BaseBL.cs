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
        private IBaseRepository<TEntity, TKey> _repository;
        protected readonly ILogger _log;
        protected readonly IMapper _mapper;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            ILogger logger,
            IMapper mapper)
        {
            _repository = repository;
            _log = logger;
            _mapper = mapper;
        }

        #region GetAllAsync
        /// <summary>
        /// Retrieves all DTOs of the specified type asynchronously.
        /// </summary>
        /// <returns>A collection of DTOs or null if none are found.</returns>
        public virtual async Task<IEnumerable<TDto>?> GetAllAsync()
        {
            _log.Information($"Starting GetAllAsync for EntityType: {typeof(TEntity).Name}.");
            try
            {
                var entities = await _repository.GetAllAsync();

                if (entities == null)
                {
                    _log.Warning($"No Entities Found During GetAllAsync for EntityType: {typeof(TEntity).Name}.");
                    return null;
                }

                var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

                _log.Information($"Completed GetAllAsync for EntityType: {typeof(TEntity).Name}. {entities.Count()} Entities Mapped to DTOs.");
                return dtos;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllAsync for EntityType: {typeof(TEntity).Name} with Message: {ex.Message}.");
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
            _log.Information($"Starting GetByIdAsync for EntityType: {typeof(TEntity).Name}.");
            try
            {
                var entity = await _repository.GetByIdAsync(key);

                if (entity == null)
                {
                    _log.Warning($"No Entity Found During GetByIdAsync for EntityType: {typeof(TEntity).Name}.");
                    return null;
                }

                var dto = _mapper.Map<TDto>(entity);

                _log.Information($"Completed GetByIdAsync. Entity Mapped to DTO of Type: {typeof(TDto).Name}.");
                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetByIdAsync for EntityType: {typeof(TEntity).Name} with Message: {ex.Message}.");
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
            _log.Information($"Starting CreateAsync for DTO of Type: {typeof(TDto).Name}.");
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                if (entity == null)
                {
                    _log.Warning($"Unable to Map DTO of Type {typeof(TDto).Name} to Entity.");
                    return null;
                }

                var createdEntity = await _repository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create Entity of Type {typeof(TEntity).Name} in Database.");
                    return null;
                }

                var resultDto = _mapper.Map<TDto>(createdEntity);

                _log.Information($"Completed CreateAsync for DTO of Type: {typeof(TDto).Name}. Entity Creation and Mapping Successful.");
                return resultDto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreateAsync for DTO of Type: {typeof(TDto).Name} with Message: {ex.Message}.");
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
            _log.Information($"Starting UpdateAsync for DTO of Type: {typeof(TDto).Name}.");
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                if (entity is null)
                {
                    _log.Warning($"Unable to map DTO to Entity. DTO Type: {typeof(TDto).Name}");
                    return null;
                }

                var results = await _repository.UpdateAsync(entity);
                if (results == null)
                {
                    _log.Warning($"Update operation failed for Entity Type: {typeof(TEntity).Name}");
                    return null;
                }

                dto = _mapper.Map<TDto>(results);

                _log.Information($"Completed UpdateAsync for DTO Type: {typeof(TDto).Name}.");
                return dto;
            }
            catch (Exception ex)
            {

                _log.Error($"Exception in UpdateAsync for DTO Type: {typeof(TDto).Name} with Message: {ex.Message}.");
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

            _log.Information($"Starting SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.SoftDeleteAsync(key);

                _log.Information($"Completed SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in SoftDeleteAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
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
            _log.Information($"Starting HardDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.HardDeleteAsync(key);

                _log.Information($"Completed HardDeleteAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in HardDeleteAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
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
            _log.Information($"Starting RestoreAsync for Entity Type: {typeof(TEntity).Name}.");
            try
            {
                var result = await _repository.RestoreAsync(key);

                _log.Information($"Completed RestoreAsync for Entity Type: {typeof(TEntity).Name}.");
                return result;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in RestoreAsync for Entity Type: {typeof(TEntity).Name} with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

    }
}
