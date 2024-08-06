using System.Security.Cryptography;
using System.Text;

namespace Intl.Realty.Firm.Helper
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out byte[] salt)
        {
            salt = new byte[32]; // Generate a 256-bit salt
            RandomNumberGenerator.Fill(salt);

            var saltedPassword = Encoding.UTF8.GetBytes(password);
            var saltedPasswordWithSalt = new byte[saltedPassword.Length + salt.Length];
            Buffer.BlockCopy(saltedPassword, 0, saltedPasswordWithSalt, 0, saltedPassword.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordWithSalt, saltedPassword.Length, salt.Length);

            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(saltedPasswordWithSalt);
                var hashBytes = new byte[salt.Length + hash.Length];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
                Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string password, string storedHashWithSalt)
        {
            // Decode the stored hash and salt
            byte[] hashBytes = Convert.FromBase64String(storedHashWithSalt);

            // Extract the salt from the stored hash (assume salt is 32 bytes)
            byte[] salt = new byte[32];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 32);

            // Extract the hash from the stored hash
            byte[] storedHash = new byte[hashBytes.Length - 32];
            Buffer.BlockCopy(hashBytes, 32, storedHash, 0, storedHash.Length);

            // Hash the input password with the extracted salt
            var saltedPassword = Encoding.UTF8.GetBytes(password);
            var saltedPasswordWithSalt = new byte[saltedPassword.Length + salt.Length];
            Buffer.BlockCopy(saltedPassword, 0, saltedPasswordWithSalt, 0, saltedPassword.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordWithSalt, saltedPassword.Length, salt.Length);

            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(saltedPasswordWithSalt);
                return storedHash.SequenceEqual(computedHash);
            }
        }

    }
}
