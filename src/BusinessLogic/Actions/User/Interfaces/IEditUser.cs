using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.User;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IEditUser : IAction
    {
        void Invoke(UserModel user);
    }
}