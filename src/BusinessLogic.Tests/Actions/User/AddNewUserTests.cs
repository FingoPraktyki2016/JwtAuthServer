using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class AddNewUserTests
    {
        [Fact]
        public void Invoke_ValidData_AddAndSaveAreCalled()
        {
            // prepare
            var userToAdd = new UserModel() { Email = "test@test.pl", Password = "test" };
            var mockedUserRepository = new Mock<IUserRepository>();

            var action = new AddNewUser(mockedUserRepository.Object);

            // action
            action.Invoke(userToAdd);

            // assert
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Once());
            mockedUserRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_InvalidData_AddNorSaveAreCalled()
        {
            // prepare
            var userToAdd = new UserModel() { };
            var mockedUserRepository = new Mock<IUserRepository>();

            var action = new AddNewUser(mockedUserRepository.Object);

            // action
            action.Invoke(userToAdd);

            // assert
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }

        [Fact]
        public void Invoke_AlreadyExists_AddNorSaveAreCalled()
        {
            // prepare
            var userToAdd = new UserModel()
            {
                Email = "test",
                Password = "Password"
            };
            var userFromDb = new DataAccess.Models.User()
            {
                Email = "test",
            };
            var findByResult = new List<DataAccess.Models.User>() { userFromDb };

            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                 .Returns(findByResult.AsQueryable());

            var action = new AddNewUser(mockedUserRepository.Object);

            // action
            action.Invoke(userToAdd);

            // assert
            mockedUserRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}