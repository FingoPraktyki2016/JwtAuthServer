using System.Linq;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class CheckUserPermissionToApp
    {
        private readonly IUserAppRepository userAppRepository;

        public CheckUserPermissionToApp(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public bool Invoke(int userId, int appId)
        {
            var appToDelete = userAppRepository.FindBy(x => x.User.Id == userId && x.App.Id == appId);

            if (appToDelete.Any())
            {
                return true;
            }

            return false;
        }
    }
}
