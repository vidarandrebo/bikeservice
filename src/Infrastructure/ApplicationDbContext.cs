using System;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes.Entities;
using BikeService.Domain.Parts.Entities;
using BikeService.Domain.Types.Entities;
using BikeService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    public DbSet<Bike> Bikes { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions conf) : base(conf)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}