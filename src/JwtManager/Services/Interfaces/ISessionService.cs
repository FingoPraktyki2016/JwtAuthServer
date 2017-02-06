namespace LegnicaIT.JwtManager.Services.Interfaces
{
    public interface ISessionService<T>
    {
        T GetItem();

        bool ContainsItem();

        bool AddItem(T item);

        bool Clear();
        
    }
}