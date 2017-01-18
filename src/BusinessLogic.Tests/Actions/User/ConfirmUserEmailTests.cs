using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class ConfirmUserEmailTests
    {
        [Fact]
        public void Invoke_ValidData_UpdatesEmailConfirmedOn()
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

        [Fact]
        public void Verify_EmailAlreadyConfirmed_SaveIsNotCalled()
        {
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                EmailConfirmedOn = DateTime.UtcNow,
            };

            DataAccess.Models.User userSaved = null;

            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);
            mockedUserRepo.Setup(r => r.Edit(It.IsAny<DataAccess.Models.User>()))
                .Callback<DataAccess.Models.User>(u => userSaved = u);

            var action = new LegnicaIT.BusinessLogic.Actions.User.Implementation.ConfirmUserEmail(mockedUserRepo.Object);

            action.Invoke(1);
            mockedUserRepo.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Verify_ForDoubledAction_UpdatesEmailConfirmedOnOnce()
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
            action.Invoke(1);

            mockedUserRepo.Verify(r => r.Save(), Times.Once());
        }
    }
}