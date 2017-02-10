using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetAllUsers : IGetAllUsers
    { 

         private readonly IUserRepository userRepository;

        public GetAllUsers(IUserRepository userRepository)
         {
            this.userRepository = userRepository;
         }

        public List<UserModel> Invoke()
        {
            var userModels = new List<UserModel>();
            var listOfUsers =  userRepository.GetAll();

            foreach (var dbUser in listOfUsers)
            {
                var userModel = new UserModel()
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    Name = dbUser.Name,
                    EmailConfirmedOn = dbUser.EmailConfirmedOn,
                    CreatedOn = dbUser.CreatedOn,
                    ModifiedOn = dbUser.ModifiedOn,
                    LockedOn = dbUser.LockedOn,
                    DeletedOn = dbUser.DeletedOn,
                };

                userModels.Add(userModel);
            }

            return userModels;
        }
    }
}
