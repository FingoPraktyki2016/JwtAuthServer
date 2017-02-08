using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using LegnicaIT.DataAccess.Models;

namespace LegnicaIT.BusinessLogic.Actions.App.Implementation
{
    public class GetUserApps : IGetUserApps
    {
        private readonly IUserAppRepository userAppRepository;
        private readonly IAppRepository appRepository;
        private readonly IUserRepository userRepository;

        public GetUserApps(IUserAppRepository userAppRepository,
            IAppRepository appRepository,
            IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.appRepository = appRepository;
            this.userRepository = userRepository;
        }

        public List<AppModel> Invoke(int userId)
        {
            var listOfApps = new List<AppModel>();
            var listApps = appRepository.GetAll();
            var user = userRepository.GetById(userId);

            List<DataAccess.Models.App> list;

            if (user != null && user.IsSuperAdmin)
            {
                list = listApps.ToList();
            }
            else
            {
                var listUserApps = userAppRepository.GetAll();

                if (!listUserApps.Any())
                {
                    // Return empty list
                    return listOfApps;
                }

                list = (from userApps in listUserApps
                        join app in listApps on userApps.App.Id equals app.Id
                        where userApps.User.Id == userId
                        select app).ToList();
            }
            
            listOfApps.AddRange(list.Select(appFromDb => new AppModel()
            {
                Id = appFromDb.Id, Name = appFromDb.Name
            }));
           

            return listOfApps;
        }
    }
}
