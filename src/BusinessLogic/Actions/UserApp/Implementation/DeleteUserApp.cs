using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class DeleteUserApp : IDeleteUserApp
    {
        private readonly IUserAppRepository appRepository;

        public DeleteUserApp(IUserAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public void Invoke(int userAppId)
        {
            var appToDelete = appRepository.GetById(userAppId);
            if (appToDelete != null)
            {
                appRepository.Delete(appToDelete);
            }
        }
    }
}