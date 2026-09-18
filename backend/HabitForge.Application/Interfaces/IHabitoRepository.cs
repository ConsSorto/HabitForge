using HabitForge.Domain.Entities;

namespace HabitForge.Application.Interfaces;

public interface IHabitoRepository
{
    Task<IEnumerable<HabitoGlobal>> GetAllAsync();
    Task<HabitoGlobal> AddAsync(HabitoGlobal habito);
}