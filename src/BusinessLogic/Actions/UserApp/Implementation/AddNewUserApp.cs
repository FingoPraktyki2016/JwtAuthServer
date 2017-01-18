using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Models.UserApp;
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
                App = appRepository.GetById(model.AppId)
            };

            if (userAppRepository.GetAll().ToList().Where(x => x.App == userApp.App && x.User == userApp.User).Count() == 0)
            {
                userApp.Role = model.Role;
                userAppRepository.Add(userApp);
                userAppRepository.Save();
            }
        }
    }
}