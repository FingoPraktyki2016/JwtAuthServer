using System;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;

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

        public void Invoke(int userId, int appId, UserRole role)
        {
            var userApp = new LegnicaIT.DataAccess.Models.UserApps()
            {
                User = userRepository.GetById(userId),
                App = appRepository.GetById(appId),
                Role = (DataAccess.Enums.UserRole)Enum.Parse(typeof(DataAccess.Enums.UserRole), role.ToString()),
            };

            if (userAppRepository.FindBy(x => x.App == userApp.App && x.User == userApp.User) == null)
            {
                userAppRepository.Add(userApp);
                userAppRepository.Save();
            }
        }
    }
}