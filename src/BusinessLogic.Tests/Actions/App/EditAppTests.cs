using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class EditAppTests
    {
        [Fact]
        public void Invoke_ValidData_ChangesName()
        {
            // prepare
            var appFromDb = new DataAccess.Models.App()
            {
                Id = 1,
                Name = "Name1",
            };

            var appToEdit = new AppModel()
            {
                Id = 1,
                Name = "Name2",
            };

            DataAccess.Models.App appSaved = null;
            var mockedAppRepository = new Mock<IAppRepository>();

            mockedAppRepository.Setup(r => r.GetById(1)).Returns(appFromDb);

            mockedAppRepository.Setup(r => r.Edit(It.IsAny<DataAccess.Models.App>()))
                .Callback<DataAccess.Models.App>(u => appSaved = u);

            var action = new EditApp(mockedAppRepository.Object);

            // action
            var actionResult = action.Invoke(appToEdit);

            // assert
            Assert.True(actionResult);
            Assert.Equal("Name2", appSaved.Name);
            mockedAppRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.App>()), Times.Once);
            mockedAppRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void Invoke_ValidData_SaveAndEditAreCalled()
        {
            // prepare
            var appFromDb = new DataAccess.Models.App()
            {
                Id = 1
            };
            var appToEdit = new AppModel()
            {
                Id = 1,
                Name = "Name1",
            };

            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.GetById(1))
                .Returns(appFromDb);

            var action = new EditApp(mockedAppRepository.Object);

            // action
            var actionResult = action.Invoke(appToEdit);

            // assert
            Assert.True(actionResult);
            mockedAppRepository.Verify(r => r.Save(), Times.Once());
            mockedAppRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.App>()), Times.Once());
        }

        [Fact]
        public void Invoke_InvalidData_SaveNorEditAreCalled()
        {
            // prepare
            var appFromDb = new DataAccess.Models.App()
            {
                Id = 1
            };
            var appToEdit = new AppModel()
            {
                Id = 1,
            };

            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.GetById(1))
                .Returns(appFromDb);

            var action = new EditApp(mockedAppRepository.Object);

            // action
            var actionResult = action.Invoke(appToEdit);

            // assert
            Assert.False(actionResult);
            mockedAppRepository.Verify(r => r.Save(), Times.Never);
            mockedAppRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.App>()), Times.Never);
        }

        [Fact]
        public void Invoke_NoAppToEdit_SaveNorEditAreCalled()
        {
            // prepare
            var appToEdit = new AppModel()
            {
                Id = 1,
                Name = "trolololo"
            };

            var mockedAppRepository = new Mock<IAppRepository>();

            var action = new EditApp(mockedAppRepository.Object);

            // action
            var actionResult = action.Invoke(appToEdit);

            // assert
            Assert.False(actionResult);
            mockedAppRepository.Verify(r => r.Save(), Times.Never);
            mockedAppRepository.Verify(r => r.Edit(It.IsAny<DataAccess.Models.App>()), Times.Never);
        }
    }
}