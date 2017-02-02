using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class DeleteUserApp : IDeleteUserApp
    {
        private readonly IUserAppRepository userAppRepository;

        public DeleteUserApp(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public bool Invoke(int userAppId)
        {
            var appToDelete = userAppRepository.GetById(userAppId);

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
