using System.Collections.Generic;
using System.Linq;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using System;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Implementation
{
    public class GetAppUsers : IGetAppUsers
    { 
        private readonly IUserAppRepository userAppRepository;
        private readonly IUserRepository userRepository;

        public GetAppUsers(IUserAppRepository userAppRepository, IUserRepository userRepository)
        {
            this.userAppRepository = userAppRepository;
            this.userRepository = userRepository;
        }

        public List<UserDetailsFromAppModel> Invoke(int appId)
        {
            var listUserApps = userAppRepository.GetAll();
            var listUser = userRepository.GetAll();
            var listOfUsers = new List<UserDetailsFromAppModel>();

            try {
                var list2 = (from userApps in listUserApps
                          join user in listUser on userApps.User.Id equals user.Id
                          where userApps.App.Id == appId select new UserDetailsFromAppModel()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Role = (UserRole) userApps.Role,

                        }).ToList();

                 listOfUsers = list2;
            }
            catch(Exception )
            {

            }

            return listOfUsers;
        }
    }
}
