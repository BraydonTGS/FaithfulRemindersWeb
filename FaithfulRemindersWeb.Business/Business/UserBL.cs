using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Business.Repository;

namespace FaithfulRemindersWeb.Business.Business
{
    /// <summary>
    /// User Business Logic
    /// </summary>
    public class UserBL : BaseBL, IUserBL
    {
        private readonly UserRepository _userRepository;

        public UserBL(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
    }
}
