namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Business Logic Base 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseBL<TDto, TKey> : IBaseBL<TDto, TKey> where TDto : BaseDto<TKey>
    {

    }
}
