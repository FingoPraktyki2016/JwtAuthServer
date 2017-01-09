using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetLastUser : IGetLastUser
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
