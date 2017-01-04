using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserExist : ICheckUserExist
    {
        private readonly IUserRepository userRepository;
        public CheckUserExist(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Invoke(string email, string password)
        {
            return userRepository.Get(email, password) != null;
        }
    }
}