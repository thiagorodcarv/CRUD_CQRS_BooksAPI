using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class GenrerRepository : IGenrerRepository
{
    protected readonly AppDbContext db;

    public GenrerRepository(AppDbContext _db)
    {
        db = _db;
    }
    public async Task<Genrer> GetGenrerById(int id)
    {
        var genrer = await db.Genrers.FindAsync(id);

        if (genrer is null)
            throw new InvalidOperationException("Genrer not found");

        return genrer;
    }

    public async Task<IEnumerable<Genrer>> GetGenrers()
    {
        var genrerlist = await db.Genrers.ToListAsync();
        return genrerlist ?? Enumerable.Empty<Genrer>();
    }

    public async Task<Genrer> AddGenrer(Genrer genrer)
    {
        if (genrer is null)
            throw new ArgumentNullException(nameof(genrer));

        await db.Genrers.AddAsync(genrer);
        return genrer;
    }

    public void UpdateGenrer(Genrer genrer)
    {
        if (genrer is null)
            throw new ArgumentNullException(nameof(genrer));

        db.Genrers.Update(genrer);
    }

    public async Task<Genrer> DeleteGenrer(int genrerId)
    {
        var genrer = await GetGenrerById(genrerId);

        if (genrer is null)
            throw new InvalidOperationException("Genrer not found");

        db.Genrers.Remove(genrer);
        return genrer;
    }
}
