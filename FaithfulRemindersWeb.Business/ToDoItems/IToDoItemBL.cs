using FaithfulRemindersWeb.Business.ToDoItems.Dto;

namespace FaithfulRemindersWeb.Business.ToDoItems
{
    /// <summary>
    /// Interface Defining Specific Implementation for ToDoItem Business Logic
    /// </summary>
    public interface IToDoItemBL
    {
        Task<ToDoItemDto> CreateNewToDoItemAsync(ToDoItemDto toDoItem);
        Task<IEnumerable<ToDoItemDto>?> GetAllToDoItemsByUserIdAsync(Guid userId);
    }
}
