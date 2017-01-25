namespace LegnicaIT.BusinessLogic.Helpers.Interfaces
{
    public interface IHasher
    {
        string CreateHash(string password, string salt);

        string GenerateRandomSalt();
    }
}