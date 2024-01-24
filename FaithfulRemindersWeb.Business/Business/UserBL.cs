using FaithfulRemindersWeb.Business.Business.Base;
using FaithfulRemindersWeb.Business.Repository;
using Microsoft.EntityFrameworkCore.Internal;

namespace FaithfulRemindersWeb.Business.Business
{
    public class UserBL : BaseBL
    {
        private readonly UserRepository _userRepository;

        public UserBL(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
    }
}
