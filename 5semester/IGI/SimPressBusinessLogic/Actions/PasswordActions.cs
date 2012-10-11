using System;
using System.Security.Cryptography;
using Entities;

namespace SimPressBusinessLogic.Actions
{
    internal static class PasswordActions
    {
        public static string GenerateSalt()
        {
            var random = new Random();
            var size = random.Next(10, 30);
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GetHashForPassword(string password, string salt)
        {
            HashAlgorithm hashGenerator = new SHA256Managed();
            var passwordWithSaltBytes = Convert.FromBase64String(password + salt);
            var hash = hashGenerator.ComputeHash(passwordWithSaltBytes);
            return Convert.ToBase64String(hash);
        }

        public static bool CheckPassword(string password, User user)
        {
            var hash = GetHashForPassword(password, user.PasswordSalt);
            return hash == user.PasswordHash;
        }

        public static bool PasswordsAreEqual(string password, string confirmPassword)
        {
            return (password == confirmPassword);
        }
    }
}
