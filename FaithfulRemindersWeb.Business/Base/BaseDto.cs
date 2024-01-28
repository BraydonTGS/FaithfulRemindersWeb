namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Base Class for All Dto(s)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDirty { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
