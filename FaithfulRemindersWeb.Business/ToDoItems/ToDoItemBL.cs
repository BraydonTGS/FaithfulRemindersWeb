using AutoMapper;
using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.ToDoItems
{
    /// <summary>
    /// ToDo Item Business Logic
    /// Responsible for Repository Interaction
    /// </summary>
    public class ToDoItemBL : BaseBL<ToDoItemDto, Guid>, IToDoItemBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;

        public ToDoItemBL(ToDoItemRepository toDoItemRepository, IMapper mapper)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region CreateNewToDoItemAsync
        /// <summary>
        /// Create a New ToDoItem
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns></returns>
        public async Task<ToDoItemDto> CreateNewToDoItemAsync(ToDoItemDto toDoItem)
        {
            try
            {
                var entity = _mapper.Map<ToDoItem>(toDoItem);

                var results = await _toDoItemRepository.CreateAsync(entity);

                var dto = _mapper.Map<ToDoItemDto>(results);

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// Query the ToDoItemRepository for all of the ToDo Items for the Specified UserId
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoItemDto>?> GetAllToDoItemsByUserIdAsync(Guid userId)
        {
            try
            {
                var entity = await _toDoItemRepository.GetAllToDoItemsByUserIdAsync(userId);

                if (entity is null) return null;

                var results = _mapper.Map<IEnumerable<ToDoItemDto>>(entity);

                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
