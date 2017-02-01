using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class GetUserDetailsTests
    {
        [Fact]
        public void Invoke_ValidData_ReturnsCorrectModel()
        {
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1,
                Name = "Name",
                Email = "email@dot.com"
            };

            // prepare
            var findByResult = new List<DataAccess.Models.User>() { userFromDb };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetUserDetails(mockedUserRepository.Object);

            // action
            var user = action.Invoke("email@dot.com");

            // check
            Assert.Equal(1, user.Id);
            Assert.Equal("Name", user.Name);
            Assert.Equal("email@dot.com", user.Email);
        }

        [Fact]
        public void Invoke_InvalidData_EmptyRepository()
        {
            // prepare
            var findByResult = new List<DataAccess.Models.User>() { };
            var mockedUserRepository = new Mock<IUserRepository>();
            mockedUserRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.User, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetUserDetails(mockedUserRepository.Object);

            // action
            var user = action.Invoke("whatislove@wp.pl");

            // check
            Assert.Null(user);
        }
    }
}
