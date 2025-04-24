using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Bikes.Entities;
using BikeService.Domain.Common;
using BikeService.Domain.Parts;
using BikeService.Domain.Parts.Entities;
using BikeService.Domain.Types;
using BikeService.Domain.Types.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<Part> Parts { get; set; }

    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<ServiceNote> ServiceNotes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}