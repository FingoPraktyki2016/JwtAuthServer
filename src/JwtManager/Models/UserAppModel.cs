using LegnicaIT.BusinessLogic.Models;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Models
{
    public class UserAppModel
    {
        public string Email;
        public string Name;
        public int Id;

        public UserAppModel(string sessionValue)
        {
            var userModel = JsonConvert.DeserializeObject<UserModel>(sessionValue);

            if (userModel != null)
            {
                Email = userModel.Email;
                Name = userModel.Name;
                Id = userModel.Id;
            }
        }

        public UserModel GetModel()
        {
            var userModel = new UserModel() { Email = this.Email, Id = this.Id, Name = this.Name };
            return userModel;
        }
    }
}