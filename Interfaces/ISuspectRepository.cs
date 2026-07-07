using ShadowTraceAPI.Entities;

namespace ShadowTraceAPI.Interfaces;

public interface ISuspectRepository
{
    Task<IEnumerable<Suspect>> GetAllAsync();

    Task<Suspect?> GetByIdAsync(int id);

    Task<Suspect> AddAsync(Suspect suspect);

    Task<Suspect?> UpdateAsync(Suspect suspect);

    Task<bool> DeleteAsync(int id);
}