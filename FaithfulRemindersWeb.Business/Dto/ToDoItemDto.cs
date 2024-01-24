using FaithfulRemindersWeb.Business.Dto.Base;

namespace FaithfulRemindersWeb.Business.Dto
{
    /// <summary>
    /// ToDo Item Data Transfer Object
    /// </summary>
    public class ToDoItemDto : BaseDto<Guid>
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public Guid UserId { get; set; }

        public UserDto? User { get; set; }
    }
}
