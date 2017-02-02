using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class DeleteApp : IDeleteApp
    {
        private readonly IAppRepository appRepository;

        public DeleteApp(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public bool Invoke(int appId)
        {
            var appToDelete = appRepository.GetById(appId);

            if (appToDelete == null)
            {
                return false;
            }

            appRepository.Delete(appToDelete);
            appRepository.Save();

            return true;
        }
    }
}
