using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetSalt(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Cannot generate salt from an empty value.");
            }

            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Cannot generate hash from an empty value.");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentException("Cannot use an empty salt from hashing value.");
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
