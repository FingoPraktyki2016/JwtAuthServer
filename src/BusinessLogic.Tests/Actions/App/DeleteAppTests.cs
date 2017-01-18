using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class DeleteAppTests
    {
        public class AddNewAppTests
        {
            [Fact]
            public void Invoke_ValidData_SaveIsCalled()
            {
                var appToDelete = new AppModel()
                {
                    Name = "haha",
                    Id = 1
                };

                var appFromDb = new DataAccess.Models.App()
                {
                    Id = 1,
                    Name = "Name1",
                };

                var mockedAppRepository = new Mock<IAppRepository>();

                mockedAppRepository.Setup(r => r.GetById(1)).Returns(appFromDb);

                var action = new DeleteApp(mockedAppRepository.Object);

                action.Invoke(appToDelete.Id);

                mockedAppRepository.Verify(r => r.Save(), Times.Once());
            }
        }
    }
}