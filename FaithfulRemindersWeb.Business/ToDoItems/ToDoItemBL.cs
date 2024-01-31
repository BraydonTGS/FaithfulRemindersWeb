using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Serilog;


namespace FaithfulRemindersWeb.Business.ToDoItems
{
    /// <summary>
    /// ToDo Item Business Logic
    /// Responsible for Repository Interaction
    /// </summary>
    public class ToDoItemBL : BaseBL<ToDoItemDto, ToDoItem, Guid>, IToDoItemBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        public ToDoItemBL(
            ToDoItemRepository toDoItemRepository,
            ILogger logger,
            IMapper mapper) : base(toDoItemRepository, logger, mapper)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository));
        }

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// Query the ToDoItemRepository for all of the ToDo Items for the Specified UserId
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoItemDto>?> GetAllToDoItemsByUserIdAsync(Guid userId)
        {
            _log.Information($"Starting GetAllToDoItemsByUserIdAsync");

            try
            {               
                var entities = await _toDoItemRepository.GetAllToDoItemsByUserIdAsync(userId);

                if (entities is null)
                {
                    _log.Warning($"No ToDoItems Found for the Specified User");
                    return null;
                }

                var results = _mapper.Map<IEnumerable<ToDoItemDto>>(entities);

                _log.Information($"Completed GetAllToDoItemsByUserIdAsync. {results.Count()} ToDoItems Found for the Specified User");

                return results;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllToDoItemsByUserIdAsync with Message {ex.Message}");

                throw;
            }
        }
        #endregion

        #region GetAllSoftDeletedToDoItemsByUserIdAsync
        /// <summary>
        /// Query the ToDoItemRepository for all of the ToDo Items for the Specified UserId
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoItemDto>?> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid userId)
        {
            _log.Information($"Starting GetAllSoftDeletedToDoItemsByUserIdAsync");
            try
            {
                var entities = await _toDoItemRepository.GetAllSoftDeletedToDoItemsByUserIdAsync(userId);

                if (entities is null)
                {
                    _log.Warning($"No Soft Deleted ToDoItems Found for the Specified User");
                    return null;
                }


                var results = _mapper.Map<IEnumerable<ToDoItemDto>>(entities);

                _log.Information($"Completed GetAllSoftDeletedToDoItemsByUserIdAsync. {results.Count()} Soft Deleted ToDoItems Found for the Specified User");

                return results;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in GetAllSoftDeletedToDoItemsByUserIdAsync with Message {ex.Message}");

                throw;
            }
        }
        #endregion
    }
}
