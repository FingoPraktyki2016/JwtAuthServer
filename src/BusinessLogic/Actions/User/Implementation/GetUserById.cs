using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetUserById : IGetUserById
    {
        private readonly IUserRepository userRepository;

        public GetUserById(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Invoke(int id)
        {
            var dbUser = userRepository.FindBy(x => x.Id == id).FirstOrDefault();
            if (dbUser == null)
            {
                return null;
            }

            var userModel = new UserModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                Name = dbUser.Name,
                EmailConfirmedOn = dbUser.EmailConfirmedOn.Value,
                CreatedOn = dbUser.CreatedOn,
                ModifiedOn = dbUser.ModifiedOn,
                LockedOn = dbUser.LockedOn.Value,
                DeletedOn = dbUser.DeletedOn.Value,
            };

            return userModel;
        }
    }
}