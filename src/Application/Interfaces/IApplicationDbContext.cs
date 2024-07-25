using System.Threading;
using System.Threading.Tasks;
using Domain.Bikes;
using Domain.Parts;
using Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<Part> Parts { get; set; }

    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}