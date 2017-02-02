using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class AddNewAppTests
    {
        [Fact]
        public void Invoke_ValidData_AddAndSaveAreCalled()
        {
            // prepare
            var appToAdd = new AppModel() { Name = "test" };
            var mockedAppRepository = new Mock<IAppRepository>();

            var action = new AddNewApp(mockedAppRepository.Object);

            // action
            action.Invoke(appToAdd);

            // assert
            mockedAppRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.App>()), Times.Once());
            mockedAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_InvalidData_AddNorSaveAreCalled()
        {
            // prepare
            var appToAdd = new AppModel();
            var mockedAppRepository = new Mock<IAppRepository>();

            var action = new AddNewApp(mockedAppRepository.Object);

            // action
            var actionResult = action.Invoke(appToAdd);

            // assert
            Assert.Equal(0, actionResult);
            mockedAppRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.App>()), Times.Never);
            mockedAppRepository.Verify(r => r.Save(), Times.Never);
        }
    }
}
