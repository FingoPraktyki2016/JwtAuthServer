using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IUserRepository userRepository;
        private readonly IUserAppRepository userAppRepository;

        public DeleteUser(IUserRepository userRepository, IUserAppRepository userAppRepository)
        {
            this.userRepository = userRepository;
            this.userAppRepository = userAppRepository;

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

            var userApps = userAppRepository.GetAll();

            var appsToDelete = (from app in userApps where app.User.Id == id select app ).ToList();
                     
            if (appsToDelete == null || appsToDelete.Count == 0)
            {
                return false;
            }

            foreach( var app in appsToDelete)
            {
                userAppRepository.Delete(app);
            }

            userAppRepository.Save();

            return true;
        }
    }
}