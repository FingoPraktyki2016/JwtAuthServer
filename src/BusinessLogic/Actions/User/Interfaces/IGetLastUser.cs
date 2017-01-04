using LegnicaIT.BusinessLogic.Models.User;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetLastUser
    {
        DataAccess.Models.User Invoke(UserModel user);
    }
}