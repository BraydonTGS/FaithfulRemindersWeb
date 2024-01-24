using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Repository;

namespace FaithfulRemindersWeb.Business.Business
{
    /// <summary>
    /// User Business Logic
    /// </summary>
    public class UserBL : BaseBL
    {
        private readonly UserRepository _userRepository;

        public UserBL(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
    }
}
