using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;

namespace FaithfulRemindersWeb.Business.ToDoItems
{
    /// <summary>
    /// Interface Defining Specific Implementation for ToDoItem Business Logic
    /// </summary>
    public interface IToDoItemBL : IBaseBL<ToDoItemDto, Guid>
    {
        Task<IEnumerable<ToDoItemDto>?> GetAllToDoItemsByUserIdAsync(Guid userId);
    }
}
