using FaithfulRemindersWeb.Business.Users.Dto;
using FluentValidation;

namespace FaithfulRemindersWeb.Business.Validation
{
    /// <summary>
    /// Validation Logic for the <see cref="UserDto"/>
    /// </summary>
    public class UserDtoValidator : BaseDtoValidator<UserDto, Guid>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Id).Must(x => x.GetType() == typeof(Guid));
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x =>x.LastName).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(250).EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
        }
    }
}
