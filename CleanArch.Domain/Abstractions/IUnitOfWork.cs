namespace CleanArch.Domain.Abstractions;

public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IGenrerRepository GenrerRepository { get; }
    Task CommitAsync();
}
