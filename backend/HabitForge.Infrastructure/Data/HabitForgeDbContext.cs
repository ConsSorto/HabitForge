using Microsoft.EntityFrameworkCore;
using HabitForge.Domain.Entities;

namespace HabitForge.Infrastructure.Data;

public class HabitForgeDbContext : DbContext {
    public HabitForgeDbContext(DbContextOptions<HabitForgeDbContext> options) : base(options) { }
    
    public DbSet<HabitoGlobal> HabitosGlobales { get; set; }
}