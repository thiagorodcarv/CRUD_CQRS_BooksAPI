using CleanArch.Domain.Abstractions;
using CleanArch.Infrastructure.Context;

namespace CleanArch.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IAuthorRepository? _authorRepo;
    private IGenrerRepository? _genrerRepo;
    private IBookRepository? _bookRepo;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IAuthorRepository AuthorRepository
    {
        get
        {
            return _authorRepo = _authorRepo ??
                new AuthorRepository(_context);
        }
    }

    public IGenrerRepository GenrerRepository
    {
        get
        {
            return _genrerRepo = _genrerRepo ??
                new GenrerRepository(_context);
        }
    }

    public IBookRepository BookRepository
    {
        get
        {
            return _bookRepo = _bookRepo ??
                new BookRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
