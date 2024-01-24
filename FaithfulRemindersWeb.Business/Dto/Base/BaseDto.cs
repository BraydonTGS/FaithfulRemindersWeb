namespace FaithfulRemindersWeb.Business.Dto.Base
{
    /// <summary>
    /// Base Class for All Dto(s)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDirty { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
