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
            var userapp1 = new DataAccess.Models.UserApps()
            {
                App = new DataAccess.Models.App() { Id = 2, Name = "app2" },
                User = new DataAccess.Models.User() { Id = 2 }
            };

            var app1 = new DataAccess.Models.App()
            { Id = 1, Name = "app1" };

            var app2 = new DataAccess.Models.App()
            { Id = 2, Name = "app2" };

            var findByResult = new List<DataAccess.Models.UserApps>() {userapp1 };
            var findByResultApp = new List<DataAccess.Models.App>() {app1, app2 };

            var mocekdUserAppRepository = new Mock<IUserAppRepository>();
            var mocekdAppRepository = new Mock<IAppRepository>();

            mocekdUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mocekdAppRepository.Setup(r => r.GetAll())
                  .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mocekdUserAppRepository.Object, mocekdAppRepository.Object);

            // action
            var list = action.Invoke(2);

            // check
            Assert.Equal(2, list.Where(x=>x.Id == 2).FirstOrDefault().Id);
            Assert.Equal("app2", list.Where(x => x.Name == "app2").FirstOrDefault().Name);
        }

        [Fact]
        public void Invoke_InvalidDataReturnsEmptyList()
        {
            // prepare
            var userapp1 = new DataAccess.Models.UserApps()
            {
                App = new DataAccess.Models.App() { Id = 1, Name = "app1" },
                User = new DataAccess.Models.User(){Id = 2 }
            };

            var app1 = new DataAccess.Models.App()
            { Id = 1, Name = "app1" };

            var findByResult = new List<DataAccess.Models.UserApps>() { userapp1 };
            var findByResultApp = new List<DataAccess.Models.App>() { app1};

            var mocekdUserAppRepository = new Mock<IUserAppRepository>();
            var mocekdAppRepository = new Mock<IAppRepository>();

            mocekdUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mocekdAppRepository.Setup(r => r.GetAll())
                  .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mocekdUserAppRepository.Object, mocekdAppRepository.Object);

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

            var app1 = new DataAccess.Models.App();

            var findByResult = new List<DataAccess.Models.UserApps>() { userapp1 };
            var findByResultApp = new List<DataAccess.Models.App>() { app1 };

            var mocekdUserAppRepository = new Mock<IUserAppRepository>();
            var mocekdAppRepository = new Mock<IAppRepository>();

            mocekdUserAppRepository.Setup(r => r.GetAll())
                .Returns(findByResult.AsQueryable);

            mocekdAppRepository.Setup(r => r.GetAll())
                  .Returns(findByResultApp.AsQueryable);

            var action = new GetUserApps(mocekdUserAppRepository.Object, mocekdAppRepository.Object);

            // action
            var list = action.Invoke(999);

            // check
            Assert.Equal(0, list.Count);
        }

    }
}
