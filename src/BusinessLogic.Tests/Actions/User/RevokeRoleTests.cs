using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.DataAccess.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class RevokeRoleTests
    {
        [Fact]
        public void Invoke_CorrectData_SavedInDatabase()
        {
            // Prepare
            var dataUserApp = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());
            mockedUserAppsRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()))
                .Callback<DataAccess.Models.UserApps>(userApps => dataUserAppSaved = userApps);

            var action = new RevokeRole(mockedUserAppsRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.User);

            // Check
            Assert.True(actionResult);
            Assert.NotEqual(UserRole.Manager, dataUserAppSaved.Role);
            Assert.Equal(UserRole.User, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Once);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void Invoke_ThisSameRole_NotSavedInDatabase()
        {
            // Prepare
            var dataUserApp = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new RevokeRole(mockedUserAppsRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.False(actionResult);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_HigherRole_NotSavedInDatabase()
        {
            // Prepare
            var dataUserApp = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new RevokeRole(mockedUserAppsRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.SuperAdmin);

            // Check
            Assert.False(actionResult);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_IncorrectUserData_NotSavedInDatabase()
        {
            // Prepare
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();

            var action = new RevokeRole(mockedUserAppsRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.User);

            // Check
            Assert.False(actionResult);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
