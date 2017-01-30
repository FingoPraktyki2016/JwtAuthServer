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
            if (password == null || email == null)
            {
                return false;
            }

            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();

            if (dbUser == null)
            {
                return false;
            }

            var hasher = new Hasher();
            var salt = dbUser.PasswordSalt;

            var hashedPassword = hasher.CreateHash(password, salt);

            return Equals(hashedPassword, dbUser.PasswordHash);
        }
    }
}