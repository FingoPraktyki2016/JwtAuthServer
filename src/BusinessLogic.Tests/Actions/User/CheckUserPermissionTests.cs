using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
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

            var mockedUserRepository = new Mock<IGetAppUserRole>();

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
           .Returns(questioned);

            var action = new CheckUserPermission(mockedUserRepository.Object);

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

            var mockedUserRepository = new Mock<IGetAppUserRole>();

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
           .Returns(questioned);

            var action = new CheckUserPermission(mockedUserRepository.Object);

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

            var mockedUserRepository = new Mock<IGetAppUserRole>();

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 2))
              .Returns(questioned);

            var action = new CheckUserPermission(mockedUserRepository.Object);

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

            var mockedUserRepository = new Mock<IGetAppUserRole>();

            mockedUserRepository.Setup(r => r.Invoke(It.IsAny<int>(), 1))
              .Returns(requestor);

            var action = new CheckUserPermission(mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 5);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_UserIsRequestorAndQuestioned_ReturnsTrue()
        {
            // prepare
            var mockedUserRepository = new Mock<IGetAppUserRole>();
            var action = new CheckUserPermission(mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 1);

            // assert
            Assert.True(allow);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var mockedUserRepository = new Mock<IGetAppUserRole>();
            var action = new CheckUserPermission(mockedUserRepository.Object);

            // action
            var allow = action.Invoke(1, 1, 3);

            // assert
            Assert.False(allow);
        }
    }
}
