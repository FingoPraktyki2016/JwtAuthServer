using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

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
            if (email == null)
            {
                return null;
            }

            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();
            if (dbUser == null)
            {
                return null;
            }

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
