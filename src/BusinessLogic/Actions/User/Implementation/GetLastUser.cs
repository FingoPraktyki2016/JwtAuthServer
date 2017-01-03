using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetLastUser : IGetLastUser
    {
        private readonly IUserRepository userRepository;

        public GetLastUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Invoke()
        {
            var user = userRepository.GetLast();

            if (user != null)
            {
                return new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    CreatedOn = user.CreatedOn,
                    DeletedOn = user.DeletedOn,
                    EmailConfirmedOn = user.EmailConfirmedOn,
                    LockedOn = user.LockedOn,
                    ModifiedOn = user.ModifiedOn
                };
            }

            return null;
        }
    }
}