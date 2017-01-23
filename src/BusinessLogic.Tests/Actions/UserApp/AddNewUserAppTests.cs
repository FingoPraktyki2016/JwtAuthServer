using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.BusinessLogic.Models.UserApp;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Tests.Actions.UserApp
{
    public class AddNewUserAppTests
    {
        [Fact]
        public void Invoke_ValidData_AddAndSaveAreCalled()
        {
            // prepare
            var userAppToAdd = new UserAppModel()
            {
                AppId = 1,
                UserId = 1,
                Role = Enums.UserRole.SuperAdmin
            };
            var findByResult = new List<DataAccess.Models.UserApps>();

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();

            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(findByResult.AsQueryable);

            var action = new AddNewUserApp(mockedUserAppRepository.Object, mockedUserRepository.Object,
                mockedAppRepository.Object);

            // action
            action.Invoke(userAppToAdd);

            // assert
            mockedUserAppRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.UserApps>()), Times.Once());
            mockedUserAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_AlreadyExists_AddNorSaveAreCalled()
        {
            // prepare
            var userAppToAdd = new UserAppModel()
            {
                AppId = 1,
                UserId = 1,
                Role = Enums.UserRole.SuperAdmin
            };
            var userAppFromDb = new DataAccess.Models.UserApps()
            {
                App = new DataAccess.Models.App() { Id = 1 },
                User = new DataAccess.Models.User() { Id = 1 },
                Role = DataAccess.Enums.UserRole.SuperAdmin
            };
            var findByResult = new List<DataAccess.Models.UserApps>() { userAppFromDb };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();

            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(findByResult.AsQueryable);

            var action = new AddNewUserApp(mockedUserAppRepository.Object, mockedUserRepository.Object,
                mockedAppRepository.Object);

            // action
            action.Invoke(userAppToAdd);

            // assert
            mockedUserAppRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}