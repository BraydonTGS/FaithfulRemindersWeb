namespace FaithfulRemindersWeb.Entity.Entities.Base
{
    /// <summary>
    /// Base Entity Object for all Entities
    /// </summary>
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public bool IsDirty { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = string.Empty;

    }
}
