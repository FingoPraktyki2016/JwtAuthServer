using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class GetUserApps : IGetUserApps
    {
        private readonly IUserAppRepository userAppRepository;

        private readonly IAppRepository appRepository;

        public GetUserApps(IUserAppRepository userAppRepository, IAppRepository appRepository)
        {
            this.userAppRepository = userAppRepository;
            this.appRepository = appRepository;
        }

        public List<AppModel> Invoke(int userId)
        {
            List<AppModel> listOfApps = new List<AppModel>();

            var listUserApps = userAppRepository.GetAll();
            var listApps = appRepository.GetAll();

            var list = (from userApps in listUserApps
                        join app in listApps on userApps.App.Id equals app.Id
                        where userApps.User.Id == userId
                        select app).ToList();

            //TODO: Automap
            foreach (var appFromDb in list)
            {
                var model = new AppModel()
                {
                    Id = appFromDb.Id,
                    Name = appFromDb.Name
                };
                listOfApps.Add(model);
            }

            return listOfApps;
        }
    }
}
