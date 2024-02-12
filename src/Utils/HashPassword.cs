using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace TrainingRestFullApi.src.Utils
{
    public class HashPassword
    {

        public byte[] GenerateSalt()
        {
            // Gere um salt aleatório
            byte[] salt = new byte[16];
            using (RNGCryptoServiceProvider rng = new())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public string GeneratePassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                MemorySize = 1024 * 1024,
                Iterations = 10
            };

            byte[] hashBytes = argon2.GetBytes(64);

            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

            return hashString;
        }

        public bool VerifyPassword(string password, string storedHash, byte[] salt)
        {
            byte[] storedHashBytes = Enumerable.Range(0, storedHash.Length)
                                             .Where(x => x % 2 == 0)
                                             .Select(x => Convert.ToByte(storedHash.Substring(x, 2), 16))
                                             .ToArray();

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                MemorySize = 1024 * 1024,
                Iterations = 10
            };
            byte[] hashBytes = argon2.GetBytes(64);
            return storedHashBytes.SequenceEqual(hashBytes);
        }
    }
}
