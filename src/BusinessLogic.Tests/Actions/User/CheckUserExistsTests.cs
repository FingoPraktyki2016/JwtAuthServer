using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class CheckUserExistsTests
    {
        [Fact]
        public void Invoke_ReturnsTrue()
        {
            // prepare
            var dataUser = new DataAccess.Models.User()
            {
                Email = "email@dot.com",
                PasswordSalt = "salt-generated",
                PasswordHash = "plain-hashed"
            };
            var findByResult = new List<DataAccess.Models.User>() { dataUser };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);

            var mockedHasher = new Mock<IHasher>();
            mockedHasher.Setup(h => h.CreateHash("plain", "salt-generated")).Returns("plain-hashed");

            var action = new CheckUserExist(mockedUserRepository.Object, mockedHasher.Object);

            // action
            var user = action.Invoke("email@dot.com", "plain");

            // check
            Assert.Equal(true, user);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsFalse()
        {
            // prepare
            var findByResult = new List<DataAccess.Models.User>();
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