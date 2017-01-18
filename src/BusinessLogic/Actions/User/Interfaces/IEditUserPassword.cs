using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.User;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IEditUserPassword : IAction
    {
        void Invoke(int userId, string plaintextPassword);
    }
}