using System.Security.Cryptography;

namespace BikeHistory.Domain.Auth {
    public static class PasswordHasher {
        public static string Hash(string password) {
            byte[] salt;
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(salt = new byte[16]);
            }
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes,0,16);
            Array.Copy(hash, 0, hashBytes,16,20);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool CheckHash(string password, string hashedPassword) {
            byte[] savedHashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(savedHashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++) {
                if (savedHashBytes[i+16] != hash[i]) {
                    return false;
                }
            }
            return true;

        }
    }
}
