using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Books.Commands;

public abstract class BookCommandBase : IRequest<Book>
{
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
    public int? GenrerId { get; set; }
}
