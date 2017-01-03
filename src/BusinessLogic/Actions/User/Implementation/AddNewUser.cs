using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class AddNewUser : IAddNewUser
    {
        private readonly IUserRepository userRepository;
        public AddNewUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Invoke()
        {
            userRepository.Add();
        }
    }
}