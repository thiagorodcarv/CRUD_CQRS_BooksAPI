using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Abstractions;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAuthors();
    Task<Author> GetAuthorById(int authorId);
    Task<Author> AddAuthor(Author author);
    void UpdateAuthor(Author author);
    Task<Author> DeleteAuthor(int authorId);
}
