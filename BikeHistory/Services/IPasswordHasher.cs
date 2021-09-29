using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BikeHistory.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BikeHistory.Services {
    public interface IPasswordHasher {
        string Hash(string password);
        bool CheckHash(string password, string hashedPassword);
    }
    public class PasswordHasher : IPasswordHasher{
        public string Hash(string password) {
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

        public bool CheckHash(string password, string hashedPassword) {
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
