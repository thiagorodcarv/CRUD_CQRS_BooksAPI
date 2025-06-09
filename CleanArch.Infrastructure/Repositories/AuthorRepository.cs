using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    protected readonly AppDbContext db;

    public AuthorRepository(AppDbContext _db)
    {
        db = _db;
    }
    public async Task<Author> GetAuthorById(int id)
    {
        var author = await db.Authors.FindAsync(id);

        if (author is null)
            throw new InvalidOperationException("Author not found");

        return author;
    }

    public async Task<IEnumerable<Author>> GetAuthors()
    {
        var authorlist = await db.Authors.ToListAsync();
        return authorlist ?? Enumerable.Empty<Author>();
    }

    public async Task<Author> AddAuthor(Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));

        await db.Authors.AddAsync(author);
        return author;
    }

    public void UpdateAuthor(Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));

        db.Authors.Update(author);
    }

    public async Task<Author> DeleteAuthor(int authorId)
    {
        var author = await GetAuthorById(authorId);

        if (author is null)
            throw new InvalidOperationException("Author not found");

        db.Authors.Remove(author);
        return author;
    }
}
