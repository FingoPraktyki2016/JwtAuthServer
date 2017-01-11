using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.User;
using LegnicaIT.DataAccess.Context;
using System.Linq;

namespace LegnicaIT.BusinessLogic.Configuration.Seeder
{
    public class JwtDbContextSeeder
    {
        public void Seed(JwtDbContext context, IAddNewUser addNewUser)
        {
            SeedUsers(context, addNewUser);
        }

        private readonly IAddNewUser addNewUser;

        public void SeedUsers(JwtDbContext context, IAddNewUser addNewUser)
        {
            for (int i = context.Users.Count(); i < 5; i++)
            {
                var model = new UserModel()
                {
                    Email = $"test{i}@test.com",
                    Password = "test",
                    Name = $"test{i}",
                };

                addNewUser.Invoke(model);
            }
        }
    }
}