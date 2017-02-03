using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.UserApp
{
    public class DeleteUserAppTests
    {
        [Fact]
        public void Invoke_ValidData_DeleteAndSaveAreCalled()
        {
            // prepare
            var userAppToDelete = new UserAppModel()
            {
                Id = 1
            };
            var userAppFromDb = new List<DataAccess.Models.UserApps>()
            {
                new DataAccess.Models.UserApps()
                {
                    Id = 1,
                    App = new DataAccess.Models.App() { Id = 123 }
                }
            };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.UserApps, bool>>>()))
               .Returns(userAppFromDb.AsQueryable());

            // action
            var action = new DeleteUserApp(mockedUserAppRepository.Object);
            var actionResult = action.Invoke(userAppToDelete.Id, 123);

            // assert
            Assert.True(actionResult);
            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Once());
            mockedUserAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_ForNullUserApps_DeleteNorSaveAreCalled()
        {
            // prepare
            var userAppToDelete = new AppModel()
            {
                Id = 1
            };
            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            // FIXME: GetById is not proper method to mock for this action
            //mockedUserAppRepository.Setup(r => r.GetById(1)).Returns((DataAccess.Models.UserApps)null);
            
            // FIXME Grzegorz Radziejewski
            var action = new DeleteUserApp(mockedUserAppRepository.Object);
            var actionResult = action.Invoke(userAppToDelete.Id, 5);

            // assert
            Assert.False(actionResult);
            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
