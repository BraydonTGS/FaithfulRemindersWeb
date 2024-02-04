using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.ToDoItems
{
    /// <summary>
    /// Interface Defining Specific Implementation for ToDoItem Business Logic
    /// </summary>
    public interface IToDoItemBL : IBaseBL<ToDoItemDto, ToDoItem, Guid>
    {
        Task<IEnumerable<ToDoItemDto>?> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid userId);
        Task<IEnumerable<ToDoItemDto>?> GetAllToDoItemsByUserIdAsync(Guid userId);
        Task<ToDoItemDto?> GetToDoItemByIdIncludeUserAsync(Guid toDoItemId);
    }
}
