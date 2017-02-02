using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class AddNewApp : IAddNewApp
    {
        private readonly IAppRepository appRepository;

        public AddNewApp(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public int Invoke(AppModel app)
        {
            if (!app.IsValid())
            {
                return 0;
            }

            var newApp = new DataAccess.Models.App()
            {
                Name = app.Name
            };

            appRepository.Add(newApp);
            appRepository.Save();

            return newApp.Id;
        }
    }
}
