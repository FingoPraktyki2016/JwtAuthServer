using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetLastUser : IGetLastUser
    {
        private readonly IUserRepository userRepository;

        public GetLastUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Invoke(UserModel user)
        {
            var userToReturn = AutoMapper.Mapper.Map<UserModel>(userRepository.GetAll().Last());
            return userToReturn;
        }
    }
}