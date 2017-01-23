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
            var action = new BusinessLogic.Actions.User.Implementation.ConfirmUserEmail(mockedUserRepo.Object);

            action.Invoke(1);

            Assert.NotNull(userSaved.EmailConfirmedOn);
        }

        [Fact]
        public void Verify_EmailAlreadyConfirmed_SaveIsNotCalled()
        {
            DateTime dateNow = DateTime.UtcNow;

            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                EmailConfirmedOn = dateNow,
            };

            var mockedUserRepo = new Mock<IUserRepository>();
            mockedUserRepo.Setup(r => r.GetById(1))
                .Returns(userFromDb);

            var action = new BusinessLogic.Actions.User.Implementation.ConfirmUserEmail(mockedUserRepo.Object);

            action.Invoke(1);

            Assert.Equal(userFromDb.EmailConfirmedOn, dateNow);
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

            var action = new BusinessLogic.Actions.User.Implementation.ConfirmUserEmail(mockedUserRepo.Object);

            action.Invoke(1);
            action.Invoke(1);

            Assert.NotNull(userSaved.EmailConfirmedOn);
            mockedUserRepo.Verify(r => r.Save(), Times.Once());
        }
    }
}