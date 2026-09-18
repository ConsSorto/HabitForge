using Microsoft.EntityFrameworkCore;
using HabitForge.Application.Interfaces;
using HabitForge.Domain.Entities;
using HabitForge.Infrastructure.Data;

namespace HabitForge.Infrastructure.Repositories;

// Implementamos la interfaz de la capa superior
public class HabitoRepository : IHabitoRepository {
    private readonly HabitForgeDbContext _context;
    
    // El constructor recibe el contexto de EF Core
    public HabitoRepository(HabitForgeDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<HabitoGlobal>> GetAllAsync() {
        return await _context.HabitosGlobales.ToListAsync(); // Consulta real a BD
    }
    
    public async Task<HabitoGlobal> AddAsync(HabitoGlobal habito) {
        await _context.HabitosGlobales.AddAsync(habito);
        
        // SaveChangesAsync envuelve la operación en una transacción SQL (COMMIT)
        await _context.SaveChangesAsync(); 
        
        return habito;
    }
}