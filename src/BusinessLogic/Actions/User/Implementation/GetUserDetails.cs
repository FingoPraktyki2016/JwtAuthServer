using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetUserDetails : IGetUserDetails
    {
        private readonly IUserRepository userRepository;

        public GetUserDetails(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Invoke(string email)
        {
            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();

            var userModel = new UserModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                Name = dbUser.Name,
            };

            return userModel;
        }
    }
}
