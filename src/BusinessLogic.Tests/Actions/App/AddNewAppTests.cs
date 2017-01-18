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
        public void Invoke_ValidData_SaveIsCalled()
        {
            var userToAdd = new AppModel() { Name = "test" };
            var mockedAppRepository = new Mock<IAppRepository>();

            var action = new AddNewApp(mockedAppRepository.Object);

            action.Invoke(userToAdd);

            mockedAppRepository.Verify(r => r.Save(), Times.Once());
        }
    }
}