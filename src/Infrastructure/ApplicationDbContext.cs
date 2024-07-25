using System;
using Application.Interfaces;
using Domain.Bikes;
using Domain.Parts;
using Domain.Types;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IApplicationDbContext
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