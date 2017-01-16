using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;

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
            var timeNow = DateTime.Now;
            var newApp = new DataAccess.Models.App()
            {
                Name = app.Name,
                CreatedOn = timeNow,
                ModifiedOn = timeNow,
            };
            appRepository.Add(newApp);
            appRepository.Save();
        }
    }
}