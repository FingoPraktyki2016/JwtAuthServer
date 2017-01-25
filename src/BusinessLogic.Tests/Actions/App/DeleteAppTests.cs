using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class DeleteAppTests
    {
        [Fact]
        public void Invoke_ValidData_DeleteAndSaveAreCalled()
        {
            // prepare
            var appToDelete = new AppModel()
            {
                Id = 1
            };
            var appFromDb = new DataAccess.Models.App()
            {
                Id = 1,
            };

            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.GetById(1)).Returns(appFromDb);

            var action = new DeleteApp(mockedAppRepository.Object);

            // action
            action.Invoke(appToDelete.Id);

            // assert
            mockedAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.App>()), Times.Once());
            mockedAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_ValidData_DeleteNorSaveAreCalled()
        {
            // prepare
            var appToDelete = new AppModel()
            {
                Id = 1
            };

            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.GetById(1)).Returns((DataAccess.Models.App)null);

            var action = new DeleteApp(mockedAppRepository.Object);

            // action
            action.Invoke(appToDelete.Id);

            // assert
            mockedAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.App>()), Times.Never);
            mockedAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}