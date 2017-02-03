using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class DeleteUserApp : IDeleteUserApp
    {
        private readonly IUserAppRepository userAppRepository;

        public DeleteUserApp(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public bool Invoke(int userId, int appId)
        {
            var appToDelete = userAppRepository.FindBy(x=>x.User.Id  == userId && x.App.Id == appId ).FirstOrDefault();

            if (appToDelete == null)
            {
                return false;
            }
            userAppRepository.Delete(appToDelete);
            userAppRepository.Save();

            return true;
        }
    }
}
