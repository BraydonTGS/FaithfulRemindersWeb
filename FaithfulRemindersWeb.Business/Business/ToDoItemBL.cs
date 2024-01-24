using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Repository;

namespace FaithfulRemindersWeb.Business.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class ToDoItemBL : BaseBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        public ToDoItemBL(ToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository)); ;
        }
    }
}
