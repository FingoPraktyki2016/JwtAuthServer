
using System.Linq;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;

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
            var userapps = userAppRepository.FindBy(x => x.User.Id == userId).Select(x => x.App.Id);
            List<AppModel> listOfApps = new List<AppModel>();

            var listUserApps = userAppRepository.FindBy(x=>x.Id != 10000);
            var listApps = appRepository.FindBy(x => x.Id != 10000);

            var list = (from e1 in listUserApps join s1 in listApps on e1.App.Id equals s1.Id where e1.User.Id == userId select s1).ToList();     
           
            foreach(var appFromDb in list)
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
