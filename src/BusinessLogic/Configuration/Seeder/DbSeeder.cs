using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models.App;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.BusinessLogic.Models.UserApp;
using LegnicaIT.DataAccess.Context;
using System;
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

        public void Seed(IAddNewUser addNewUser, IAddNewApp addNewApp, IAddNewUserApp addNewUserApps)
        {
            SeedUsers(addNewUser);
            SeedApps(addNewApp);
            SeedUserApps(addNewUserApps);
        }

        private readonly string[] users = { "superadmin", "manager", "user" };

        public void SeedUsers(IAddNewUser addNewUser)
        {
            foreach (var user in users)
            {
                var model = new UserModel()
                {
                    Email = $"{user}@test.com",
                    Password = "test",
                    Name = user,
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

        public void SeedUserApps(IAddNewUserApp addNewUserApps)
        {
            foreach (var user in users)
            {
                var model = new UserAppModel()
                {
                    AppId = context.Apps.OrderBy(r => Guid.NewGuid()).Take(1).FirstOrDefault().Id,
                    UserId = context.Users.FirstOrDefault(r => r.Name.Contains(user)).Id
                };

                switch (user)
                {
                    case "superadmin":
                        model.Role = UserRole.SuperAdmin;
                        addNewUserApps.Invoke(model);

                        break;

                    case "manager":
                        model.Role = UserRole.Manager;
                        addNewUserApps.Invoke(model);
                        break;

                    case "user":
                        model.Role = UserRole.User;
                        addNewUserApps.Invoke(model);
                        break;
                }
            }
        }
    }
}
