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
    public class ToDoItemBL : BaseBL<ToDoItemDto, ToDoItem, Guid>, IToDoItemBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;

        public ToDoItemBL(ToDoItemRepository toDoItemRepository, IMapper mapper) : base(toDoItemRepository, mapper)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
