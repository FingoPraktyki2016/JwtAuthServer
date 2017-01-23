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

        public void Invoke(int userAppId)
        {
            var appToDelete = userAppRepository.GetById(userAppId);
            if (appToDelete != null)
            {
                userAppRepository.Delete(appToDelete);
                userAppRepository.Save();
            }
        }
    }
}