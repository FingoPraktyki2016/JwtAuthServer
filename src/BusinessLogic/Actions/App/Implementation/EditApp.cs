using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models;
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

        public bool Invoke(AppModel app)
        {
            if (!app.IsValid())
            {
                return false;
            }

            var appToEdit = appRepository.GetById(app.Id);

            if (appToEdit == null)
            {
                return false;
            }

            appToEdit.Name = app.Name;

            appRepository.Edit(appToEdit);
            appRepository.Save();

            return true;
        }
    }
}
