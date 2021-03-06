﻿using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System;
using System.Linq;
using LegnicaIT.BusinessLogic.Models;

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

        public int Invoke(UserAppModel model)
        {
            var userApp = new DataAccess.Models.UserApps()
            {
                User = userRepository.GetById(model.UserId),
                App = appRepository.GetById(model.AppId),
                Role = (DataAccess.Enums.UserRole)Enum.Parse(typeof(DataAccess.Enums.UserRole), model.Role.ToString()),
            };

            if (userApp.User == null ||
                userApp.App == null ||
                userAppRepository.FindBy(x => x.App.Id == userApp.App.Id && x.User.Id == userApp.User.Id).Any())
            {
                return 0;
            }

            userAppRepository.Add(userApp);
            userAppRepository.Save();

            return userApp.Id;
        }
    }
}
