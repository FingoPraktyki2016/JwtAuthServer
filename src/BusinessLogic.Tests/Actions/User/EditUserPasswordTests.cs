using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class EditUserPasswordTests
    {
        [Fact]
        public void Invoke_ValidData_ChangesPasswordSaltAndHash()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                Name = "Name",
                PasswordHash = "hash",
                PasswordSalt = "salt"
            };

            DataAccess.Models.User userSaved = null;
            var mockedUserRepo = new Mock<IUserRepository>();

            mockedUserRepo.Setup(r => r.GetById(1)).Returns(userFromDb);
            mockedUserRepo.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(u => userSaved = u);

            var mockedHasher = new Mock<IHasher>();
            mockedHasher.Setup(h => h.GenerateRandomSalt()).Returns("salt-generated");
            mockedHasher.Setup(h => h.CreateHash("plain", "salt-generated")).Returns("plain-hashed");

            var action = new EditUserPassword(mockedUserRepo.Object, mockedHasher.Object);

            // action
            action.Invoke(1, "plain");

            // assert
            Assert.Equal("plain-hashed", userSaved.PasswordHash);
            Assert.Equal("salt-generated", userSaved.PasswordSalt);
            Assert.Equal("Name", userSaved.Name);
        }

        [Fact]
        public void Invoke_ValidData_SaveIsCalled()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User() { Id = 1 };
            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);

            var action = new EditUserPassword(mockedUserRepo.Object);

            // action
            action.Invoke(1, "plain");

            // assert
            // TODO: Add more asserts
            mockedUserRepo.Verify(r => r.Save(), Times.Once());
        }
    }
}
