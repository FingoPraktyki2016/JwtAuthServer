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
        public void Invoke_ValidData_SaveIsCalled()
        {
            var appToAdd = new UserAppModel()
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

            action.Invoke(appToAdd);

            mockedUserAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_AlreadyExists_SaveIsNotCalled()
        {
            var appToAdd = new UserAppModel()
            {
                AppId = 1,
                UserId = 1,
                Role = Enums.UserRole.SuperAdmin
            };

            var appFromDb = new DataAccess.Models.UserApps()
            {
                App = new DataAccess.Models.App() { Id = 1 },
                User = new DataAccess.Models.User() { Id = 1 },
                Role = DataAccess.Enums.UserRole.SuperAdmin
            };
            var findByResult = new List<DataAccess.Models.UserApps>() { appFromDb };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();

            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(findByResult.AsQueryable);

            var action = new AddNewUserApp(mockedUserAppRepository.Object, mockedUserRepository.Object,
                mockedAppRepository.Object);

            action.Invoke(appToAdd);

            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
