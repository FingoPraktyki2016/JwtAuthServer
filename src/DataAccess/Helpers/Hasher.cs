using System;
using System.Security.Cryptography;

namespace LegnicaIT.DataAccess.Helpers
{
    public static class Hasher
    {
        private static int keySize = 192; //256 chars
        private static int saltSize = 96; //128 chars
        private static int iterations = 10000;

        public static string CreateHash(string password, byte[] salt)
        {
            if(password.Length > 0 && salt != null)
            {
                var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
                var key = deriveBytes.GetBytes(keySize);
                return Convert.ToBase64String(key);
            }

            return null;
        }

        public static byte[] GenerateRandomSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buffor = new byte[saltSize];
            rng.GetBytes(buffor);
            return buffor;
        }

        public static bool VerifyHashedPassword(string newPasswordHash, string dbPasswordHash)
        {
            return Equals(newPasswordHash, dbPasswordHash);
        }
    }
}
