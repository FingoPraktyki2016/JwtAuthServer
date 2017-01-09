using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetLastUser
    {
        private readonly IUserRepository userRepository;

        public GetLastUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public DataAccess.Models.User Invoke(UserModel user)
        {
            return userRepository.GetAll().Last();
        }
    }
}
