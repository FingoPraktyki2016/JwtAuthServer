using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Enums;
using LegnicaIT.DataAccess.Models;
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
            var dataUserApp = new UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());
            mockedUserAppsRepository.Setup(r => r.Edit(It.IsAny<UserApps>()))
                .Callback<UserApps>(userApps => dataUserAppSaved = userApps);

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.User);

            // Check
            Assert.True(actionResult);
            Assert.NotEqual(UserRole.Manager, dataUserAppSaved.Role);
            Assert.Equal(UserRole.User, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Once);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Once);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
        }

        [Fact]
        public void Invoke_ThisSameRole_NotSavedInDatabase()
        {
            // Prepare
            var dataUserApp = new UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.False(actionResult);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
        }

        [Theory]
        [InlineData(UserRole.User, Enums.UserRole.Manager)]
        [InlineData(UserRole.Manager, Enums.UserRole.SuperAdmin)]
        public void Invoke_HigherRole_NotSavedInDatabase(UserRole oldRole, Enums.UserRole newRole)
        {
            // Prepare
            var dataUserApp = new UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = oldRole
            };
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var getAllResults = new List<UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, newRole);

            // Check
            Assert.False(actionResult);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
        }

        [Fact]
        public void Invoke_LowerRoleThanSuperAdminNotInApp_SavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();

            var savedUser = dataUser;

            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(user => savedUser = user);

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.True(actionResult);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Once);
            mockedUserRepository.Verify(r => r.Save(), Times.Once);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Once);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            Assert.False(savedUser.IsSuperAdmin);
        }

        [Fact]
        public void Invoke_LowerRoleThanSuperAdminInApp_SavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var dataUserApp = new UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.Manager
            };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();

            var savedUser = dataUser;
            var dataUserAppSaved = dataUserApp;
            var getUserAppResults = new List<UserApps> { dataUserApp };

            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(user => savedUser = user);
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getUserAppResults.AsQueryable());
            mockedUserAppsRepository.Setup(r => r.Edit(It.IsAny<UserApps>()))
                .Callback<UserApps>(userApp => dataUserAppSaved = userApp);

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.True(actionResult);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Once);
            mockedUserRepository.Verify(r => r.Save(), Times.Once);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            Assert.False(savedUser.IsSuperAdmin);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
        }

        [Fact]
        public void Invoke_FromSuperAdminToSuperAdmin_NotSavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var getAllResults = new List<UserApps>();
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.SuperAdmin);

            // Check
            Assert.False(actionResult);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
        }

        [Fact]
        public void Invoke_IncorrectUserData_NotSavedInDatabase()
        {
            // Prepare
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedAddNewUserApp = new Mock<IAddNewUserApp>();

            var action = new RevokeRole(mockedUserAppsRepository.Object,
                mockedUserRepository.Object,
                mockedAddNewUserApp.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.User);

            // Check
            Assert.False(actionResult);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedAddNewUserApp.Verify(r => r.Invoke(It.IsAny<UserAppModel>()), Times.Never);
        }
    }
}
