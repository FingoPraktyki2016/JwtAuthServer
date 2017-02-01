namespace LegnicaIT.JwtManager.Models
{
    public class FormModel<T>
    {
        public T Model { get; set; }
        public bool IsEdit { get; set; }

        public FormModel(T model, bool isEdit = false)
        {
            Model = model;
            IsEdit = isEdit;
        }
    }
}