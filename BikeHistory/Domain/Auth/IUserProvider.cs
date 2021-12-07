using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

/*
namespace BikeHistory.Services {
    public interface IUserProvider {
        Task AddUser(User user);
        Task<User> GetUser(int id);
    }
    public class UserProvider : IUserProvider{
        private IPasswordHasher _hasher;
        private BikeContext _context;
        public async Task AddUser(User user) {
            user.Password = _hasher.Hash(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUser(int id) {
            return await _context.Users.FindAsync(id);
        }

        public UserProvider(BikeContext context, IPasswordHasher hasher) {
            _context = context;
            _hasher = hasher;
        }
    }
}
*/