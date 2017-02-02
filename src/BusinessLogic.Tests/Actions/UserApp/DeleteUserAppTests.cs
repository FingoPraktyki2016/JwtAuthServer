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
            var userAppFromDB = new DataAccess.Models.UserApps()
            {
                Id = 1,
            };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();
            mockedUserAppRepository.Setup(r => r.GetById(1)).Returns(userAppFromDB);

            var action = new DeleteUserApp(mockedUserAppRepository.Object);

            // action
            var actionResult = action.Invoke(userAppToDelete.Id);

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
            mockedUserAppRepository.Setup(r => r.GetById(1)).Returns((DataAccess.Models.UserApps) null);

            var action = new DeleteUserApp(mockedUserAppRepository.Object);

            // action
            var actionResult = action.Invoke(userAppToDelete.Id);

            // assert
            Assert.False(actionResult);
            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
