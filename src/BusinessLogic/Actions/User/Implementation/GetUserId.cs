using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GetUserId : IGetUserId
    {
        private readonly IUserRepository userRepository;

        public GetUserId(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int Invoke(string email)
        {
            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();

            return dbUser?.Id ?? 0;
        }
    }
}