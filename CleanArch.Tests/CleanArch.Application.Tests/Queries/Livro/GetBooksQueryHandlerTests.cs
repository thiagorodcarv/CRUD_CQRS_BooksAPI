using Moq;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Abstractions;
using MediatR;
using CleanArch.Application.Books.Queries;
using static CleanArch.Application.Books.Queries.GetBooksQuery;

public class GetBooksQueryHandlerTests
{
    [Fact]
    public async Task Should_Return_Books_When_Exists()
    {

        var repoMock = new Mock<IUnitOfWork>();
        var mediatorMock = new Mock<IMediator>();

        repoMock.Setup(r => r.BookRepository.GetBooks()).ReturnsAsync(new List<Book> { new Book("Titulo", 1, 1) });

        var handler = new GetBooksQueryHandler(repoMock.Object);
        var result = await handler.Handle(new GetBooksQuery(), CancellationToken.None);

        Assert.NotEmpty(result);
    }
}
