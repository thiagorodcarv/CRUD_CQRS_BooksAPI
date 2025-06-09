using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Abstractions;

public interface IGenrerRepository
{
    Task<IEnumerable<Genrer>> GetGenrers();
    Task<Genrer> GetGenrerById(int genrerId);
    Task<Genrer> AddGenrer(Genrer genrer);
    void UpdateGenrer(Genrer genrer);
    Task<Genrer> DeleteGenrer(int genrerId);
}
