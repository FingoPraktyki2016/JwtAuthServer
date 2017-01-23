using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models.App;
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
            var appToAdd = new AppModel() { Name = "test" };
            var mockedAppRepository = new Mock<IAppRepository>();

            var action = new AddNewApp(mockedAppRepository.Object);

            action.Invoke(appToAdd);

            mockedAppRepository.Verify(r => r.Add(It.IsAny<DataAccess.Models.App>()), Times.Once());
            mockedAppRepository.Verify(r => r.Save(), Times.Once());
        }
    }
}