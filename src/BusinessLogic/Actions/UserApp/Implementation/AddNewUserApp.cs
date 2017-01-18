using System;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Models.UserApp;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class AddNewUserApp : IAddNewUserApp
    {
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;
        private readonly IAppRepository appRepository;

        public AddNewUserApp(IUserAppRepository userAppRepository, IUserRepository userRepository, IAppRepository appRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
            this.appRepository = appRepository;
        }

        public void Invoke(UserAppModel model)
        {
            var userApp = new LegnicaIT.DataAccess.Models.UserApps()
            {
                User = userRepository.GetById(model.UserId),
                App = appRepository.GetById(model.AppId),
                Role = (DataAccess.Enums.UserRole)Enum.Parse(typeof(DataAccess.Enums.UserRole), model.Role.ToString()),
            };

            if (userAppRepository.GetAll().Any(x => x.App == userApp.App && x.User == userApp.User))
            {
                userAppRepository.Add(userApp);
                userAppRepository.Save();
            }
        }
    }
}