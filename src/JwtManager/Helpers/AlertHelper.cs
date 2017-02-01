using System.Collections.Generic;
using LegnicaIT.JwtManager.Models;

namespace LegnicaIT.JwtManager.Helpers
{
    public class AlertHelper
    {
        public List<AlertModel> Alerts = new List<AlertModel>();

        public List<AlertModel> GetAlerts()
        {
            return Alerts;
        }

        public void Success(string message, string title = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                Title = title,
                Type = AlertModel.Class.Success
            });
        }

        public void Info(string message, string title = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                Title = title,
                Type = AlertModel.Class.Info
            });
        }

        public void Warning(string message, string title = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                Title = title,
                Type = AlertModel.Class.Warning
            });
        }

        public void Danger(string message, string title = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                Title = title,
                Type = AlertModel.Class.Danger
            });
        }
    }
}
