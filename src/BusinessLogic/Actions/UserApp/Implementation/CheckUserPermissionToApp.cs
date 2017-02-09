using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;
using UserRole = LegnicaIT.DataAccess.Enums.UserRole;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class CheckUserPermissionToApp : ICheckUserPermissionToApp
    {
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;

        public CheckUserPermissionToApp(IUserAppRepository userAppRepository, IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
        }

        public bool Invoke(int userId, int appId, ActionType type = ActionType.Display)
        {
            var user = userRepository.GetById(userId);

            if (user == null)
            {
                return false;
            }

            if (user.IsSuperAdmin)
            {
                return true;
            }

            var appToQuestion = userAppRepository.FindBy(x => x.User.Id == userId && x.App.Id == appId).FirstOrDefault();
            switch (type)
            {
                case ActionType.EditDelete:
                    if (appToQuestion != null)
                    {
                        return (appToQuestion.Role == UserRole.Manager);
                    }
                    break;

                case ActionType.Display:
                    return userAppRepository.FindBy(x => x.User.Id == userId && x.App.Id == appId).Any();
            }
            return false;
        }
    }
}
