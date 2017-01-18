using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class EditApp : IEditApp
    {
        private readonly IAppRepository appRepository;

        public EditApp(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public void Invoke(AppModel app)
        {
            var appToEdit = appRepository.GetById(app.Id);
            if (appToEdit != null)
            {
                appToEdit.Name = app.Name;

                appRepository.Edit(appToEdit);
                appRepository.Save();
            }
        }
    }
}