using System.Linq;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class GetApp : IGetApp
    {
        private readonly IAppRepository appRepository;

        public GetApp(IAppRepository appRepository)
        {
            this.appRepository = appRepository;
        }

        public AppModel Invoke(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return null;
            }

            var app = appRepository.FindBy(x => x.Id == id).FirstOrDefault();

            if (app == null)
            {
                return null;
            }

            var appModel = new AppModel()
            {
                Id = app.Id,
                Name = app.Name,
            };

            return appModel;
        }
    }
}