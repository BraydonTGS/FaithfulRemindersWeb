using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.ToDoItems.Dto
{
    /// <summary>
    /// ToDo Item Data Transfer Object
    /// </summary>
    public class ToDoItemDto : BaseDto<Guid>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; } = null;

        public bool IsCompleted { get; set; }

        public Guid UserId { get; set; }

        public UserDto? User { get; set; }
    }
}
