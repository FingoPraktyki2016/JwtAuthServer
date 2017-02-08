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
    // TODO: Tests for SuperAdmin
    public class GrantRoleTests
    {
        [Fact]
        public void Invoke_CorrectData_SavedInDatabase()
        {
            // Prepare
            var dataUserApp = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.User
            };
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());
            mockedUserAppsRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()))
                .Callback<DataAccess.Models.UserApps>(userApps => dataUserAppSaved = userApps);
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.True(actionResult);
            Assert.NotEqual(UserRole.User, dataUserAppSaved.Role);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Once);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Once);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_FromManagerToSuperAdmin_SavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var dataUserSaved = dataUser;
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(user => dataUserSaved = user);

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.SuperAdmin);

            // Check
            Assert.True(actionResult);
            Assert.True(dataUserSaved.IsSuperAdmin);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Once);
            mockedUserRepository.Verify(r => r.Save(), Times.Once);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_FromSuperAdminToSuperAdmin_NotSavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var dataUserSaved = dataUser;
            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(user => dataUserSaved = user);

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.SuperAdmin);

            // Check
            Assert.False(actionResult);
            Assert.True(dataUserSaved.IsSuperAdmin);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
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
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = false
            };
            var dataUserAppSaved = dataUserApp;
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.False(actionResult);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_LowerRole_NotSavedInDatabase()
        {
            // Prepare
            var dataUserApp = new DataAccess.Models.UserApps
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
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.User);

            // Check
            Assert.False(actionResult);
            Assert.Equal(UserRole.Manager, dataUserAppSaved.Role);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_LowerRoleThanSuperAdmin_NotSavedInDatabase()
        {
            // Prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var dataUserApp = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
                Role = UserRole.User
            };
            var dataUserSaved = dataUser;
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(dataUser);
            mockedUserRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(user => dataUserSaved = user);
            var getAllResults = new List<DataAccess.Models.UserApps> { dataUserApp };
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            mockedUserAppsRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
                .Returns(getAllResults.AsQueryable());

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.False(actionResult);
            Assert.True(dataUserSaved.IsSuperAdmin);
            mockedUserRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_IncorrectUserData_NotSavedInDatabase()
        {
            // Prepare
            var mockedUserAppsRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();

            var action = new GrantRole(mockedUserAppsRepository.Object, mockedUserRepository.Object);

            // Action
            var actionResult = action.Invoke(1, 1, Enums.UserRole.Manager);

            // Check
            Assert.False(actionResult);
            mockedUserAppsRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppsRepository.Verify(r => r.Save(), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
