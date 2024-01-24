using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Business.ToDoItems;
using AutoMapper;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Business Logic Base 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseBL<TDto, TKey> where TDto : BaseDto<TKey>
    {
   
    }
}
