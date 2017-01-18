using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class EditAppTests
    {
        [Fact]
        public void Invoke_ValidData_ChangesPasswordSaltAndHash()
        {
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
            var mockedAppRepo = new Mock<IAppRepository>();

            mockedAppRepo.Setup(r => r.GetById(1)).Returns(appFromDb);

            mockedAppRepo.Setup(r => r.Edit(It.IsAny<DataAccess.Models.App>()))
                .Callback<DataAccess.Models.App>(u => appSaved = u);

            var action = new EditApp(mockedAppRepo.Object);
            action.Invoke(appToEdit);

            Assert.Equal("Name2", appSaved.Name);
        }

        [Fact]
        public void Invoke_ValidData_SaveIsCalled()
        {
            var appFromDb = new DataAccess.Models.App()
            {
                Id = 1
            };

            var appToEdit = new AppModel()
            {
                Id = 1,
                Name = "Name1",
            };

            var mochedAppRepo = new Mock<IAppRepository>();
            mochedAppRepo.Setup(r => r.GetById(1))
                .Returns(appFromDb);
            var action = new EditApp(mochedAppRepo.Object);

            action.Invoke(appToEdit);

            mochedAppRepo.Verify(r => r.Save(), Times.Once());
        }
    }
}