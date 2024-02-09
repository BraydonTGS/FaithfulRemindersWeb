using FluentValidation;

namespace FaithfulRemindersWeb.Business.Validation
{
    /// <summary>
    /// Validation Logic for Password that can be entered by the User.
    /// </summary>
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x).MaximumLength(50);
            RuleFor(x => x).MinimumLength(8);
        }
    }
}
