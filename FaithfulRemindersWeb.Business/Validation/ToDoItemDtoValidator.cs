using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FluentValidation;

namespace FaithfulRemindersWeb.Business.Validation
{
    /// <summary>
    /// Validation Logic for the <see cref="ToDoItemDto"/>
    /// </summary>
    public class ToDoItemDtoValidator : BaseDtoValidator<ToDoItemDto, Guid>
    {
        public ToDoItemDtoValidator()
        {
            RuleFor(x => x.Id).Must(x => x.GetType() == typeof(Guid));
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).MinimumLength(0).MaximumLength(250);      
        }
    }
}
