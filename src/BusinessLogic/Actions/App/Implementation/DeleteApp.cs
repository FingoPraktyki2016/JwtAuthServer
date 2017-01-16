using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class DeleteApp : IDeleteApp
    {
        private readonly IAppRepository appRepository;

        public DeleteApp(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public void Invoke(int id)
        {
            var appToEdit = appRepository.GetById(id);

            var timeNow = DateTime.UtcNow;

            appRepository.Edit(appToEdit);
            appRepository.Save();
        }
    }
}