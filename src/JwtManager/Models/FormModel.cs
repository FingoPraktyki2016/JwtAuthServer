namespace LegnicaIT.JwtManager.Models
{
    public class FormModel<T>
    {
        public bool IsEdit { get; set; }
        public T Model { get; set; }

        public FormModel(bool isEdit, T model)
        {
            this.IsEdit = isEdit;
            this.Model = model;
        }
    }
}