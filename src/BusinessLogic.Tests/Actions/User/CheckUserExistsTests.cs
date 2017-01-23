using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class CheckUserExistsTests
    {
        [Fact]
        public void Invoke_ReturnsTrue()
        {
            // prepare
            var hasher = new Hasher();
            var salt = hasher.GenerateRandomSalt();
            var dataUser = new DataAccess.Models.User() { Email = "email@dot.com", PasswordSalt = salt, PasswordHash = hasher.CreateHash("test", salt) };
            var findByResult = new List<DataAccess.Models.User>() { dataUser };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new CheckUserExist(mockedUserRepository.Object);

            // action
            var user = action.Invoke("email@dot.com", "test");

            // check
            Assert.Equal(true, user);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var findByResult = new List<DataAccess.Models.User>() { };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new CheckUserExist(mockedUserRepository.Object);

            // action
            var user = action.Invoke("email@dot.com", "test");

            // check
            Assert.Equal(false, user);
        }
    }
}