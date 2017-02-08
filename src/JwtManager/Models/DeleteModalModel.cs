namespace LegnicaIT.JwtManager.Models
{
    public class DeleteModalModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }

        public DeleteModalModel()
        {
        }

        public DeleteModalModel(string action, string controller)
        {
            this.Action = action;
            this.Controller = controller;
        }
    }
}
