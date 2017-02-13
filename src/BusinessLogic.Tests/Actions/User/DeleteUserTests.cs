using LegnicaIT.BusinessLogic.Actions.User.Implementation;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LegnicaIT.BusinessLogic.Tests.Actions.User
{
    public class DeleteUserTests
    {
        [Fact]
        public void Invoke_ValidData_DeleteAndSaveAreCalled()
        {
            // prepare
            var userToDelete = new UserModel()
            {
                Id = 1
            };
            var userFromDB = new DataAccess.Models.User()
            {
                Id = 1,
            };

            var userapp1 = new DataAccess.Models.UserApps
            {
                App = new DataAccess.Models.App
                {
                    Id = 1,
                    Name = "app1"
                },
                User = new DataAccess.Models.User { Id = 1 }
            };

            var applist = new List<DataAccess.Models.UserApps> { userapp1 };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();

            mockedUserRepository.Setup(r => r.GetById(1)).Returns(userFromDB);

            mockedUserAppRepository.Setup(r => r.GetAll()).Returns(applist.AsQueryable);


            var action = new DeleteUser(mockedUserRepository.Object, mockedUserAppRepository.Object);

            // action
            var actionResult = action.Invoke(userToDelete.Id);

            // assert
            Assert.True(actionResult);
            mockedUserRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.User>()), Times.Once());
            mockedUserRepository.Verify(r => r.Save(), Times.Once());
            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Once());
            mockedUserAppRepository.Verify(r => r.Save(), Times.Once());
        }

        [Fact]
        public void Invoke_ValidData_DeleteNorSaveAreCalled()
        {
            // prepare
            var userToDelete = new UserModel();

            var userFromDB = new DataAccess.Models.User()
            {
                Id = 2,
            };

            var userapp1 = new DataAccess.Models.UserApps
            {
                App = new DataAccess.Models.App
                {
                    Id = 1,
                    Name = "app1"
                },
                User = new DataAccess.Models.User { Id = 3 }
            };

            var applist = new List<DataAccess.Models.UserApps> { userapp1 };

            var mockedUserRepository = new Mock<IUserRepository>();
            var mockedUserAppRepository = new Mock<IUserAppRepository>();


            mockedUserRepository.Setup(r => r.GetById(1)).Returns(userFromDB);

            mockedUserAppRepository.Setup(r => r.GetAll()).Returns(applist.AsQueryable);

            var action = new DeleteUser(mockedUserRepository.Object, mockedUserAppRepository.Object);

            // action
            var actionResult = action.Invoke(userToDelete.Id);

            // assert
            Assert.False(actionResult);
            mockedUserRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.User>()), Times.Never);
            mockedUserRepository.Verify(r => r.Save(), Times.Never);
            mockedUserAppRepository.Verify(r => r.Delete(It.IsAny<DataAccess.Models.UserApps>()), Times.Never);
            mockedUserAppRepository.Verify(r => r.Save(), Times.Never);
        }
        
    }
}
