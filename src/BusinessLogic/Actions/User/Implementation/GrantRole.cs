using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;

namespace LegnicaIT.BusinessLogic.Actions.User.Implementation
{
    public class GrantRole : IRevokeRole
    {
        private readonly IUserAppRepository userAppRepository;

        public GrantRole(IUserAppRepository userAppRepository)
        {
            this.userAppRepository = userAppRepository;
        }

        public void Invoke(int appId, int user, UserRole newRole)
        {
            var userApp = userAppRepository.GetAll().FirstOrDefault(m => m.User.Id == user && m.App.Id == appId);

            try
            {
                var userRole = userApp.Role;

                //TODO: fix user role namespace
                //if (userRole.HasRole(newRole))
                //{
                //    userApp.Role = userRole.AddRole(newRole);
                //    userAppRepository.Edit(userApp);
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
