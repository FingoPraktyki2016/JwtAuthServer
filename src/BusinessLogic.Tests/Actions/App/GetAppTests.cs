using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LegnicaIT.BusinessLogic.Actions.App.Implementation;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.App
{
    public class GetAppTests
    {
        [Fact]
        public void Invoke_ReturnsCorrectModel()
        {
            // prepare
            var dataApp = new DataAccess.Models.App() { Id = 1234, Name = "TestApp" };
            var findByResult = new List<DataAccess.Models.App> { dataApp };
            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.App, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetApp(mockedAppRepository.Object);

            // action
            var app = action.Invoke(1234);

            // check
            Assert.Equal(1234, app.Id);
            Assert.Equal("TestApp", app.Name);
        }

        [Fact]
        public void Invoke_ForInvalidId_ReturnsNulledModel()
        {
            // prepare
            var findByResult = new List<DataAccess.Models.App>();
            var mockedAppRepository = new Mock<IAppRepository>();
            mockedAppRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<DataAccess.Models.App, bool>>>()))
                .Returns(findByResult.AsQueryable);
            var action = new GetApp(mockedAppRepository.Object);

            // action
            var app = action.Invoke(999);

            // check
            Assert.Null(app);
        }
    }
}
