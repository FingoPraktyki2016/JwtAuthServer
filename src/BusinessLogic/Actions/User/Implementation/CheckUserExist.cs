using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class CheckUserExist : ICheckUserExist
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;

        public CheckUserExist(
            IUserRepository userRepository,
            IHasher hasher = null)
        {
            this.userRepository = userRepository;
            this.hasher = hasher ?? new Hasher();
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

            var salt = dbUser.PasswordSalt;
            var hashedPassword = hasher.CreateHash(password, salt);

            return Equals(hashedPassword, dbUser.PasswordHash);
        }
    }
}