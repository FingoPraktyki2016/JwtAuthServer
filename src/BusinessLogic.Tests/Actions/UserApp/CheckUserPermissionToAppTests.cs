using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.UserApp
{
    public class CheckUserPermissionToAppTests
    {
        [Fact]
        public void Invoke_ValidData_ReturnsTrue()
        {
            // prepare
            var userAppFromDb = new DataAccess.Models.UserApps()
            {
                User = new DataAccess.Models.User() { Id = 1 },
                App = new DataAccess.Models.App() { Id = 1 },
            };

            var findByResult = new List<DataAccess.Models.UserApps>() { userAppFromDb };
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
              .Returns(findByResult.AsQueryable);
            var action = new CheckUserPermissionToApp(mockedUserAppRepository.Object);

            // action
            var allow = action.Invoke(1, 1);

            // assert
            Assert.NotNull(userAppFromDb);
            Assert.Equal(true, allow);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var action = new CheckUserPermissionToApp(mockedUserAppRepository.Object);

            // action
            var allow = action.Invoke(1, 1);

            // assert
            Assert.Equal(false, allow);
        }
    }
}
