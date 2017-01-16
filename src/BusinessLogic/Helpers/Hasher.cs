using System;
using System.Security.Cryptography;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public static class Hasher
    {
        private static int keySize = 192; //256 chars
        private static int saltSize = 96; //128 chars
        private static int iterations = 10000;

        public static string CreateHash(string password, string salt)
        {
            if (password.Length > 0 && salt != null)
            {
                var byteArraySalt = Convert.FromBase64String(salt);
                var deriveBytes = new Rfc2898DeriveBytes(password, byteArraySalt, iterations);
                var key = deriveBytes.GetBytes(keySize);
                return Convert.ToBase64String(key);
            }
            return null;
        }

        public static string GenerateRandomSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buffor = new byte[saltSize];
            rng.GetBytes(buffor);
            return Convert.ToBase64String(buffor);
        }
    }
}