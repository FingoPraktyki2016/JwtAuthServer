using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using LegnicaIT.DataAccess.Enums;

namespace LegnicaIT.BusinessLogic.Tests.Actions.UserApp
{
    public class GetAppUsersTests
    {
        [Fact]
        public void Invoke_ValidDataReturnsCorrectModel()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps
            {
                Id= 1,
                App = new DataAccess.Models.App { Id = 1 },
                User = new DataAccess.Models.User { Id = 1 },
                Role = UserRole.Manager
            };

            var userapp2 = new DataAccess.Models.UserApps
            {
                Id = 2,
                App = new DataAccess.Models.App { Id = 2 },
                User = new DataAccess.Models.User { Id = 2 },
                Role = UserRole.SuperAdmin
            };

            var user = new DataAccess.Models.User
            {
                Id = 1,
                Name = "User1",
                Email = "User1@gmail.com" 
            };

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1, userapp2 };
            var findByResultApp = new List<DataAccess.Models.User> { user };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedUserRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetAppUsers(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(1);
            
            // check
            Assert.Equal(1, list.FirstOrDefault(x => x.Id == 1).Id);
            Assert.Equal("User1", list.FirstOrDefault(x => x.Name == "User1").Name);
            Assert.Equal("User1@gmail.com", list.FirstOrDefault(x => x.Email == "User1@gmail.com").Email);
            //TODO Check UserRole (Comparing two roles doesn't work)
        }

        [Fact]
        public void Invoke_InvalidDataReturnsEmptyList()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps
            {
                Id = 1,
                App = new DataAccess.Models.App { Id = 1 },
                User = new DataAccess.Models.User { Id = 1 },
                Role = UserRole.Manager
            };

            var user = new DataAccess.Models.User
            {
                Id = 1,
                Name = "User1",
                Email = "User1@gmail.com"
            };

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1 };
            var findByResultApp = new List<DataAccess.Models.User> { user };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedUserRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetAppUsers(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(999);

            // check
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Invoke_EmptyDataReturnsEmptyList()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps();

            var user = new DataAccess.Models.User();

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1 };
            var findByResultApp = new List<DataAccess.Models.User> { user };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedUserRepository = new Mock<IUserRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedUserRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetAppUsers(mockedUserAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(999);

            // check
            Assert.Equal(0, list.Count);
        }

    }
}
