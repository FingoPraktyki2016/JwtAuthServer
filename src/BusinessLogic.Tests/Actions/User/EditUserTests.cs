using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class EditUserTests
    {
        [Fact]
        public void Invoke_ValidData_UpdatesName()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                Name = "Name",
            };
            var userUpdated = new UserModel()
            {
                Id = 1,
                Name = "Name2", // updated value
            };

            DataAccess.Models.User userSaved = null;

            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);
            mockedUserRepo.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(u => userSaved = u);
            var action = new EditUser(mockedUserRepo.Object);

            // action
            action.Invoke(userUpdated);

            // assert
            Assert.Equal("Name2", userSaved.Name);
        }

        [Fact]
        public void Invoke_ValidData_SaveIsCalled()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User() { Id = 1 };
            var userUpdated = new UserModel() { Id = 1, Name = "Name" };
            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);
            var action = new EditUser(mockedUserRepo.Object);

            // action
            action.Invoke(userUpdated);

            // assert
            mockedUserRepo.Verify(r => r.Save(), Times.Once());
        }
    }
}