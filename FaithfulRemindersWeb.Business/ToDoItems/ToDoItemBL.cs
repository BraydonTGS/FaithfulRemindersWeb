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
            try
            {
                var entities = await _toDoItemRepository.GetAllToDoItemsByUserIdAsync(userId);

                if (entities is null) return null;

                var results = _mapper.Map<IEnumerable<ToDoItemDto>>(entities);

                return results;
            }
            catch (Exception)
            {
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
            try
            {
                var entities = await _toDoItemRepository.GetAllSoftDeletedToDoItemsByUserIdAsync(userId);

                if (entities is null) return null;

                var results = _mapper.Map<IEnumerable<ToDoItemDto>>(entities);

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
