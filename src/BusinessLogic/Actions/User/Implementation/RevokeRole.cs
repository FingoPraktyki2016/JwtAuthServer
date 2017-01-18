using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class RevokeRole : IRevokeRole
    {
        private readonly IUserAppRepository userAppRepository;

        public RevokeRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public void Invoke(int appId, int user, UserRole removeRole)
        {
            var userApp = userAppRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

            try
            {
                var userRole = userApp.Role;

                //TODO: fix user role namespace
                //if (userRole.HasRole(removeRole))
                //{
                //    userApp.Role = userRole.RemoveRole(removeRole);
                //    userAppRepository.Edit(userRole);
                //    userAppRepository.Save();
                //}
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
