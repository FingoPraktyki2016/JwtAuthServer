using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Context;
using System.Linq;
using LegnicaIT.BusinessLogic.Actions.Role.Interfaces;
using LegnicaIT.BusinessLogic.Models.Role;

namespace LegnicaIT.BusinessLogic.Configuration.Seeder
{
    public class JwtDbContextSeeder
    {
        private readonly JwtDbContext context;

        public JwtDbContextSeeder(JwtDbContext context)
        {
            this.context = context;
        }

        public void Seed(IAddNewUser addNewUser, IAddNewApp addNewApp/*, IAddNewRole addNewRole*/)
        {
            SeedUsers(addNewUser);
            SeedApps(addNewApp);
            // Delete after database reorganization
            // TODO: test with ID
            //SeedRoles(addNewRole);
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

        public void SeedRoles(IAddNewRole addNewRole)
        {
            // Prepare
            var superAdmin = new RoleModel()
            {
                //Id = 1,
                Name = "SuperAdmin"
            };

            var appManager = new RoleModel()
            {
                //Id = 2,
                Name = "AppManager"
            };

            var appUser = new RoleModel()
            {
                //Id = 3,
                Name = "AppUser"
            };

            // Seed this
            addNewRole.Invoke(superAdmin);
            addNewRole.Invoke(appManager);
            addNewRole.Invoke(appUser);
        }
    }
}
