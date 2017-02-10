using LegnicaIT.BusinessLogic.Actions.Base;
using System.Threading.Tasks;

namespace LegnicaIT.BusinessLogic.Helpers.Interfaces
{
    public interface IEmailService : IAction
    {
        Task SendEmailAsync(string emailAddress, string subject, string message);
    }
}
