using CleanArch.Domain.Abstractions;
using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    protected readonly AppDbContext db;

    public BookRepository(AppDbContext _db)
    {
        db = _db;
    }
    public async Task<Book> GetBookById(int id)
    {
        var book = await db.Books.FindAsync(id);

        if (book is null)
            throw new InvalidOperationException("Book not found");

        return book;
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        var booklist = await db.Books.ToListAsync();
        return booklist ?? Enumerable.Empty<Book>();
    }

    public async Task<IEnumerable<Book>> GetBooks(int pageNumber, int pageSize)
    {
        var books = await db.Books
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return books ?? Enumerable.Empty<Book>();
    }

    public async Task<PagedResult<BookDto>> SearchByString(string str, int page, int pageSize)
    {
        var query = from book in db.Books
                    join author in db.Authors on book.AuthorId equals author.Id
                    join genrer in db.Genrers on book.GenrerId equals genrer.Id
                    where book.Title.Contains(str)
                        || author.Name.Contains(str)
                        || genrer.Name.Contains(str)
                    select new BookDto { 
                        Id = book.Id,
                        Title = book.Title,
                        Author = author.Name,
                        Genrer = genrer.Name,
                    };

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<BookDto>(items, total, page, pageSize);
    }

    public async Task<PagedResult<BookDto>> GetAllPaged(int page,
                                                        int pageSize,
                                                        string? ordedBy = "Titulo",
                                                        string? direction = "asc")
    {
       var query = from livro in db.Books
                    join autor in db.Authors on livro.AuthorId equals autor.Id
                    join genero in db.Genrers on livro.GenrerId equals genero.Id
                    select new BookDto
                    {
                        Id = livro.Id,
                        Title = livro.Title!,
                        Author = autor.Name,
                        Genrer = genero.Name
                    };

        query = (ordedBy?.ToLower(), direction?.ToLower()) switch
        {
            ("titulo", "desc") => query.OrderByDescending(l => l.Title),
            ("autor", "asc") => query.OrderBy(l => l.Author),
            ("autor", "desc") => query.OrderByDescending(l => l.Author),
            ("genero", "asc") => query.OrderBy(l => l.Genrer),
            ("genero", "desc") => query.OrderByDescending(l => l.Genrer),
            _ => query.OrderBy(l => l.Title)
        };

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResult<BookDto>(items, total, page, pageSize);
    }

    public async Task<Book> AddBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        await db.Books.AddAsync(book);
        return book;
    }

    public void UpdateBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        db.Books.Update(book);
    }

    public async Task<Book> DeleteBook(int bookId)
    {
        var book = await GetBookById(bookId);

        if (book is null)
            throw new InvalidOperationException("Book not found");

        db.Books.Remove(book);
        return book;
    }
}
