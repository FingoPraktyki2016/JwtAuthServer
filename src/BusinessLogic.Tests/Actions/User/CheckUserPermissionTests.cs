using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class CheckUserPermissionTests
    {
        [Fact]
        public void Invoke_HigherRole_ReturnsTrue()
        {
            // prepare

            var requestor = UserRole.Manager;
            var questioned = UserRole.User;

            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
           .Returns(questioned);

            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 2);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_EqualRole_ReturnsFalse()
        {
            // prepare
            var requestor = UserRole.User;
            var questioned = UserRole.User;

            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
           .Returns(questioned);

            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 2);

            // assert
            Assert.False(allow);
        }

        [Fact]
        public void Invoke_LowerRole_ReturnsFalse()
        {
            // prepare
            var requestor = UserRole.User;
            var questioned = UserRole.Manager;

            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
              .Returns(questioned);

            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 2);

            // assert
            Assert.False(allow);
        }

        [Fact]
        public void Invoke_RequestorIsSuperAdmin_ReturnsTrue()
        {
            // prepare
            var requestor = UserRole.SuperAdmin;

            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedGetAppUserRoleRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 5);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_UserIsRequestorAndQuestioned_ReturnsTrue()
        {
            // prepare
            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();

            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 1);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var mockedGetAppUserRoleRepository = new Mock<IGetAppUserRole>();
            var mockedUserRepository = new Mock<IUserRepository>();
            var action = new CheckUserPermission(mockedGetAppUserRoleRepository.Object, mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 3);

            // assert
            Assert.False(allow);
        }
    }
}
