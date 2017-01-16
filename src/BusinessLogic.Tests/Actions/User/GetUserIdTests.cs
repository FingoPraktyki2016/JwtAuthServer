using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class GetUserIdTests
    {
        [Fact]
        public void Invoke_ReturnsCorrectId()
        {
            // prepare
            var dataUser = new DataAccess.Models.User() { Id = 1234 };
            var findByResult = new List<DataAccess.Models.User>() { dataUser };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetUserId(mockedUserRepository.Object);

            // action
            var userId = action.Invoke("email@dot.com");

            // check
            Assert.Equal(1234, userId);
        }

        [Fact]
        public void Invoke_EmptyRepository_ReturnsZero()
        {
            // prepare
            var dataUser = new DataAccess.Models.User();
            var findByResult = new List<DataAccess.Models.User>() { dataUser };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetUserId(mockedUserRepository.Object);

            // action
            var userId = action.Invoke("email@dot.com");

            // check
            Assert.Equal(0, userId);
        }
    }
}
