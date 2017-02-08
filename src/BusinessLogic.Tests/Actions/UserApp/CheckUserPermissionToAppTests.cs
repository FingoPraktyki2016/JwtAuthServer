using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.UserApp
{
    public class CheckUserPermissionToAppTests
    {
        [Fact]
        public void Invoke_ValidData_ReturnsTrue()
        {
            // prepare
            var userAppFromDb = new DataAccess.Models.UserApps
            {
                User = new DataAccess.Models.User { Id = 1 },
                App = new DataAccess.Models.App { Id = 1 },
            };
            var userFromDb = new DataAccess.Models.User();

            var findByResult = new List<DataAccess.Models.UserApps> { userAppFromDb };
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(userFromDb);
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
              .Returns(findByResult.AsQueryable);
            var action = new CheckUserPermissionToApp(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_ForSuperAdmin_ReturnsTrue()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(userFromDb);
            var action = new CheckUserPermissionToApp(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var action = new CheckUserPermissionToApp(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1);

            // assert
            Assert.False(allow);
        }
    }
}
