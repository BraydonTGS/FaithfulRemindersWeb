using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Dto;
using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Business.Repository;

namespace FaithfulRemindersWeb.Business.Business
{
    /// <summary>
    /// ToDo Item Business Logic
    /// Responsible for Repository Interaction
    /// </summary>
    public class ToDoItemBL : BaseBL, IToDoItemBL
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        public ToDoItemBL(ToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository ?? throw new ArgumentNullException(nameof(toDoItemRepository)); ;
        }

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ToDoItemDto>> GetAllToDoItemsByUserIdAsync()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }           
        }
        #endregion
    }
}
