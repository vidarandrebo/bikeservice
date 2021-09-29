using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BikeHistory.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BikeHistory.Services {
    public interface IBikeProvider {
        Task AddBike(Bike bike);
        Task<Bike> GetBike(int id);
    }
    public class BikeProvider : IBikeProvider {
        private BikeContext _context;
        public async Task AddBike(Bike bike) {
            await _context.Bikes.AddAsync(bike);
            await _context.SaveChangesAsync();
            
        }
        public async Task<Bike> GetBike(int id) {
            return await _context.Bikes.FindAsync(id);
        }
            
        public BikeProvider(BikeContext context) {
            _context = context;
        }
    }
}
