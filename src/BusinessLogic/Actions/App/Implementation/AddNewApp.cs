using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models.App;
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

        public void Invoke(AppModel app)
        {
            if (app.IsValid())
            {
                var newApp = new DataAccess.Models.App()
                {
                    Name = app.Name
                };
                appRepository.Add(newApp);
                appRepository.Save();
            }
        }
    }
}