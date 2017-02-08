using LegnicaIT.BusinessLogic.Helpers.Interfaces;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace LegnicaIT.BusinessLogic.Helpers
{
    public class Hasher : IHasher
    {
        private const int KeySize = 192; //256 chars
        private const int SaltSize = 96; //128 chars
        private const int Iterations = 10000;

        public string CreateHash(string password, string salt)
        {
            try
            {
                if (password.Length > 0 && password.Length > 0)
                {
                    var byteArraySalt = Convert.FromBase64String(salt);
                    var deriveBytes = new Rfc2898DeriveBytes(password, byteArraySalt, Iterations);
                    var key = deriveBytes.GetBytes(KeySize);

                    return Convert.ToBase64String(key);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }

        public string GenerateRandomSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var buffor = new byte[SaltSize];
            rng.GetBytes(buffor);

            return Convert.ToBase64String(buffor);
        }
    }
}
