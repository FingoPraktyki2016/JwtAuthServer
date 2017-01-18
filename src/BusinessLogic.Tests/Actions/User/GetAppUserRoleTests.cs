using System.Collections.Generic;
using LegnicaIT.DataAccess.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class GetAppUserRoleTests
    {
        [Fact]
        public void Invoke_ReturnsCorrectRoleName()
        {
            // prepare
            var dataUserAppRole = new UserAppRole()
            {
                App = new App() { Id = 1 },
                User = new UserApps() { Id = 2 },
                Role = new Role() { Name = "TestRole" }
            };

            var getAllResult = new List<UserAppRole>() { dataUserAppRole };
            var mockedUserRoleRepository = new Mock<IUserRoleRepository>();
            mockedUserRoleRepository.Setup(r => r.GetAll())
                .Returns(getAllResult);
            var action = new BusinessLogic.Actions.User.Implementation.GetAppUserRole(mockedUserRoleRepository.Object);

            // action
            var roleName = action.Invoke(1, 2);

            // check
            Assert.Equal("TestRole", roleName);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsNull()
        {
            // prepare
            var dataUserAppRole = new UserAppRole();

            var getAllResult = new List<UserAppRole>() { dataUserAppRole };
            var mockedUserRoleRepository = new Mock<IUserRoleRepository>();
            mockedUserRoleRepository.Setup(r => r.GetAll())
                .Returns(getAllResult);
            var action = new BusinessLogic.Actions.User.Implementation.GetAppUserRole(mockedUserRoleRepository.Object);

            // action
            var roleName = action.Invoke(1, 2);

            // check
            Assert.Null(roleName);
        }

        [Fact]
        public void Invoke_EmptyRole_ReturnsNull()
        {
            // prepare
            var dataUserAppRole = new UserAppRole()
            {
                App = new App() { Id = 1 },
                User = new UserApps() { Id = 2 },
            };

            var getAllResult = new List<UserAppRole>() { dataUserAppRole };
            var mockedUserRoleRepository = new Mock<IUserRoleRepository>();
            mockedUserRoleRepository.Setup(r => r.GetAll())
                .Returns(getAllResult);
            var action = new BusinessLogic.Actions.User.Implementation.GetAppUserRole(mockedUserRoleRepository.Object);

            // action
            var roleName = action.Invoke(1, 2);

            // check
            Assert.Null(roleName);
        }
    }
}
