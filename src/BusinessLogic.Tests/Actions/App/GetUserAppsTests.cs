using Moq;
using System.Collections.Generic;
using Xunit;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.App.Implementation;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class GetUserAppsTests
    {
        [Fact]
        public void Invoke_ValidDataReturnsCorrectModel()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps
            {
                App = new DataAccess.Models.App
                {
                    Id = 2,
                    Name = "app2"
                },
                User = new DataAccess.Models.User { Id = 2 }
            };

            var app1 = new DataAccess.Models.App
            {
                Id = 1,
                Name = "app1"
            };

            var app2 = new DataAccess.Models.App
            {
                Id = 2,
                Name = "app2"
            };

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1 };
            var findByResultApp = new List<DataAccess.Models.App> { app1, app2 };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedAppRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mockedUserAppRepository.Object, mockedAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(2);

            // check
            Assert.Equal(1, list.Count);
            Assert.Equal(2, list.FirstOrDefault(x => x.Id == 2).Id);
            Assert.Equal("app2", list.FirstOrDefault(x => x.Name == "app2").Name);
        }

        [Fact]
        public void Invoke_ValidDataForSuperAdmin_ReturnsAllApps()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps
            {
                App = new DataAccess.Models.App
                {
                    Id = 2,
                    Name = "app2"
                },
                User = new DataAccess.Models.User
                {
                    Id = 2,
                    IsSuperAdmin = true
                }
            };

            var app1 = new DataAccess.Models.App
            {
                Id = 1,
                Name = "app1"
            };

            var app2 = new DataAccess.Models.App
            {
                Id = 2,
                Name = "app2"
            };

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1 };
            var findByResultApp = new List<DataAccess.Models.App> { app1, app2 };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();

            mockedUserRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(userapp1.User);

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedAppRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mockedUserAppRepository.Object, mockedAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(2);

            // check
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Invoke_InvalidDataReturnsEmptyList()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps
            {
                App = new DataAccess.Models.App { Id = 1, Name = "app1" },
                User = new DataAccess.Models.User {Id = 2 }
            };

            var app1 = new DataAccess.Models.App
            {
                Id = 1, Name = "app1"
            };

            var findByResult = new List<DataAccess.Models.UserApps> { userapp1 };
            var findByResultApp = new List<DataAccess.Models.App> { app1};

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedAppRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mockedUserAppRepository.Object, mockedAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(999);

            // check
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Invoke_EmptyDataReturnsEmptyList()
        {
            // prepare
            var findByResult = new List<DataAccess.Models.UserApps>();
            var findByResultApp = new List<DataAccess.Models.App>();

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            var mockedAppRepository = new Mock<IAppRepository>();

            mockedUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mockedAppRepository.Setup(r => r.GetAll())
                .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mockedUserAppRepository.Object, mockedAppRepository.Object, mockedUserRepository.Object);

            // action
            var list = action.Invoke(999);

            // check
            Assert.Equal(0, list.Count);
        }
    }
}
