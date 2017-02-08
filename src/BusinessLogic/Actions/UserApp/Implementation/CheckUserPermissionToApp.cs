using System.Linq;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class CheckUserPermissionToApp : ICheckUserPermissionToApp
    {
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;

        public CheckUserPermissionToApp(IUserAppRepository userAppRepository, IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
        }

        public bool Invoke(int userId, int appId)
        {
            var user = userRepository.GetById(userId);

            if (user == null)
            {
                return false;
            }

            if (user.IsSuperAdmin)
            {
                return true;
            }

            var appToQuestion = userAppRepository.FindBy(x => x.User.Id == userId && x.App.Id == appId);

            return appToQuestion.Any();
        }
    }
}
