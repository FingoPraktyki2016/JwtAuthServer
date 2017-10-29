using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class GetAllUsersTest
    {

        [Fact]
        public void Invoke_ValidDataReturnsUserList()
        {
            // prepare
            var userFromDb = new DataAccess.Models.User()
            {
                Id = 1
            };

            var userList = new List<DataAccess.Models.User> { userFromDb };

            var mockedUserRepository = new Mock<IUserRepository>();

            mockedUserRepository.Setup(r => r.GetAll()).
                Returns(userList.AsQueryable);

            var action = new GetAllUsers(mockedUserRepository.Object);

            // action
            var list = action.Invoke();

            // check
            Assert.Equal(1, list.FirstOrDefault(x => x.Id == 1).Id);
            Assert.Single(list);
        }

        [Fact]
        public void Invoke_EmptyDataReturnsEmptyList()
        {
            // prepare
            var userList = new List<DataAccess.Models.User>();

            var mockedUserRepository = new Mock<IUserRepository>();

            mockedUserRepository.Setup(r => r.GetAll()).
                Returns(userList.AsQueryable);

            var action = new GetAllUsers(mockedUserRepository.Object);

            // action
            var list = action.Invoke();

            // check
            Assert.Empty(list);
         }

    }

}
