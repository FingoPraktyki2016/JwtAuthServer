using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserExist : ICheckUserExist
    {
        private readonly IUserRepository userRepository;

        public CheckUserExist(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Invoke(string email, string password)
        {
            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();
            if (dbUser != null)
            {
                var salt = dbUser.PasswordSalt;
                var hashedPassword = Hasher.CreateHash(password, salt);
                if (Equals(hashedPassword, dbUser.PasswordHash))
                {
                    return true;
                }
            }
            return false;
        }
    }
}