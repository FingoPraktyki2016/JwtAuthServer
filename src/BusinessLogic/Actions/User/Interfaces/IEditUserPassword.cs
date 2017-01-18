using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IEditUserPassword : IAction
    {
        void Invoke(int userId, string plaintextPassword);
    }
}
