using FaithfulRemindersWeb.Business.Base;
using FluentValidation;

namespace FaithfulRemindersWeb.Business.Validation
{
    /// <summary>
    /// Base Validation Logic to be shared across any object that is <see cref="BaseDto{TKey}"/>
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseDtoValidator<TDto, TKey> : AbstractValidator<TDto> where TDto : BaseDto<TKey>
    {
        public BaseDtoValidator()
        {

        }
    }
}
