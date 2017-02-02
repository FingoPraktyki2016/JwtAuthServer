using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class DeleteUserTests
    {
        [Fact]
        public void Invoke_ValidData_DeleteAndSaveAreCalled()
        {
            // prepare
            var userToDelete = new UserModel()
            {
                Id = 1
            };
            var userFromDB = new DataAccess.Models.User()
            {
                Id = 1,
            };

            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(1)).Returns(userFromDB);

            var action = new DeleteUser(mockedUserRepository.Object);

            // action
            var actionResult = action.Invoke(userToDelete.Id);

            // assert
            Assert.True(actionResult);
            mockedUserRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.User>()), Times.Once());
            mockedUserRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_ValidData_DeleteNorSaveAreCalled()
        {
            // prepare
            var userToDelete = new UserModel()
            {
                Id = 1
            };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.GetById(1)).Returns((DataAccess.Models.User)null);

            var action = new DeleteUser(mockedUserRepository.Object);

            // action
            var actionResult = action.Invoke(userToDelete.Id);

            // assert
            Assert.False(actionResult);
            mockedUserRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
