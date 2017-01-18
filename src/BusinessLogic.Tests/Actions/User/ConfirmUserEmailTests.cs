using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class ConfirmUserEmailTests
    {
        [Fact]
        public void Invoke_ValidData_UpdatesName()
        {
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                EmailConfirmedOn = null,
            };

            DataAccess.Models.User userSaved = null;

            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);
            mockedUserRepo.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(u => userSaved = u);
            var action = new LegnicaIT.BusinessLogic.Actions.User.Implementation.ConfirmUserEmail(mockedUserRepo.Object);

            action.Invoke(1);

            Assert.NotNull(userSaved.EmailConfirmedOn);
        }
    }
}