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

        public void Invoke(int appId)
        {
            var appToEdit = appRepository.GetById(appId);
            if (appToEdit != null)
            {
                appRepository.Edit(appToEdit);
                appRepository.Save();
            }
        }
    }
}