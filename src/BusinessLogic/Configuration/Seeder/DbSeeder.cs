using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Context;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Configuration.Seeder
{
    public class JwtDbContextSeeder
    {
        private readonly JwtDbContext context;

        public JwtDbContextSeeder(JwtDbContext context)
        {
            this.context = context;
        }

        public void Seed(IAddNewUser addNewUser, IAddNewApp addNewApp)
        {
            SeedUsers(addNewUser);
            SeedApps(addNewApp);
        }

        public void SeedUsers(IAddNewUser addNewUser)
        {
            for (int i = context.Users.Count(); i < 5; i++)
            {
                var model = new UserModel()
                {
                    Email = $"test{i + 1}@test.com",
                    Password = "test",
                    Name = $"test{i + 1}",
                };

                addNewUser.Invoke(model);
            }
        }

        public void SeedApps(IAddNewApp addNewApp)
        {
            for (int i = context.Apps.Count(); i < 5; i++)
            {
                var model = new AppModel()
                {
                    Name = $"App{i + 1}"
                };

                addNewApp.Invoke(model);
            }
        }
    }
}
