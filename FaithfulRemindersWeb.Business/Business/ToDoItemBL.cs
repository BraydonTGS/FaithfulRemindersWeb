using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Dto;
using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Business.Repository;

namespace FaithfulRemindersWeb.Business.Business
{
    /// <summary>
    /// ToDo Item Business Logic
    /// </summary>
    public class ToDoItemBL : BaseBL, IToDoItemBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        public ToDoItemBL(ToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository)); ;
        }
    }
}
