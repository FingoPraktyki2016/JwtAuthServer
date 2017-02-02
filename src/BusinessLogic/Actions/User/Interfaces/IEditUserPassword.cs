using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IEditUserPassword : IAction
    {
        bool Invoke(int userId, string plaintextPassword);
    }
}
