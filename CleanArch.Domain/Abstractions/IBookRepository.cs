using CleanArch.Domain.DTOs;
using CleanArch.Domain.Entities;


namespace CleanArch.Domain.Abstractions;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooks();
    Task<IEnumerable<Book>> GetBooks(int pageNumber, int pageSize);
    Task<Book> GetBookById(int bookId);
    Task<Book> AddBook(Book book);
    void UpdateBook(Book book);
    Task<Book> DeleteBook(int bookId);
    Task<PagedResult<BookDto>> SearchByString(string str, int page, int pageSize);
    Task<PagedResult<BookDto>> GetAllPaged(int page, int pageSize, string? ordedBy = "Titulo", string? direction = "asc");
}