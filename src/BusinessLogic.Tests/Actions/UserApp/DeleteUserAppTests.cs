using LegnicaIT.BusinessLogic.Actions.UserApp.Implementation;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.BusinessLogic.Models.UserApp;
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

            action.Invoke(userAppToDelete.Id);

            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Once());
            mockedUserAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_ValidData_DeleteNorSaveAreCalled()
        {
            var userAppToDelete = new AppModel()
            {
                Id = 1
            };

            var mockedUserAppRepository = new Mock<IUserAppRepository>();

            mockedUserAppRepository.Setup(r => r.GetById(1)).Returns((DataAccess.Models.UserApps)null);

            var action = new DeleteUserApp(mockedUserAppRepository.Object);

            action.Invoke(userAppToDelete.Id);

            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}