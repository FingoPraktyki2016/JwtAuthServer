using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IUserRepository userRepository;

        public DeleteUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Invoke(int id)
        {
            var userToDelete = userRepository.GetById(id);

            if (userToDelete == null)
            {
                return false;
            }

            userRepository.Delete(userToDelete);
            userRepository.Save();

            return true;
        }
    }
}