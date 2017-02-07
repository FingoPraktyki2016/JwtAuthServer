using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.DataAccess.Enums;
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
            var dataUser = new DataAccess.Models.User();
            var dataUserApp = new UserApps
            {
                Role = UserRole.Manager
            };

            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(dataUser);

            var getUserAppResult = new List<UserApps> { dataUserApp };
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getUserAppResult.AsQueryable());
            var action = new BusinessLogic.Actions.User.Implementation
                .GetAppUserRole(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var actionResult = action.Invoke(1, 2);

            // check
            Assert.Equal("Manager", actionResult.ToString());
        }

        [Fact]
        public void Invoke_ForSuperAdmin_ReturnsSuperAdmin()
        {
            // prepare
            var dataUser = new DataAccess.Models.User
            {
                IsSuperAdmin = true
            };
            var dataUserApp = new UserApps
            {
                Role = UserRole.Manager
            };

            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(dataUser);

            var getUserAppResult = new List<UserApps> { dataUserApp };
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getUserAppResult.AsQueryable());
            var action = new BusinessLogic.Actions.User.Implementation
                .GetAppUserRole(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var actionResult = action.Invoke(1, 2);

            // check
            Assert.Equal("SuperAdmin", actionResult.ToString());
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsNone()
        {
            // prepare
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(new DataAccess.Models.User());

            var getUserAppResult = new List<UserApps>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<UserApps, bool>>>()))
                .Returns(getUserAppResult.AsQueryable());
            var action = new BusinessLogic.Actions.User.Implementation
                .GetAppUserRole(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var actionResult = action.Invoke(1, 2);

            // check
            Assert.Equal("None", actionResult.ToString());
        }
    }
}