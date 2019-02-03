using System;
using System.Security.Cryptography;

namespace MicroServices.Services.Identity.Domain.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int SALT_SIZE = 40;
        private static readonly int ITERATIONS_COUNT = 10_000;

        public string GetHash(string value, string salt)
        {
            var pbkdf = new Rfc2898DeriveBytes(value, GetBytes(value), ITERATIONS_COUNT);

            return Convert.ToBase64String(pbkdf.GetBytes(SALT_SIZE));
        }

        public string GetSalt(string value)
        {
            var saltBytes = new byte[SALT_SIZE];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
