using Moq;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Abstractions;
using FluentValidation;
using MediatR;
using CleanArch.Application.Books.Commands;
using static CleanArch.Application.Books.Commands.CreateBookCommand;

public class CreateBookHandlerTests
{
    [Fact]
    public async Task Shoudl_Create_Book_When_Valid()
    {

        var repoMock = new Mock<IUnitOfWork>();
        var validatorMock = new Mock<IValidator<CreateBookCommand>>();
        var mediatorMock = new Mock<IMediator>();


        repoMock.Setup(a => a.AuthorRepository.GetAuthorById(1)).ReturnsAsync(new Author(1,"Claudio"));
        repoMock.Setup(a => a.GenrerRepository.GetGenrerById(2)).ReturnsAsync(new Genrer(1, "Suspense")); ;

        var handler = new CreateBookCommandHandler(repoMock.Object, validatorMock.Object, mediatorMock.Object);

        var command = new CreateBookCommand
        {
            Title = "Livro Teste",
            AuthorId = 1,
            GenrerId = 2
        };

        var result = await handler.Handle(command, CancellationToken.None);

        repoMock.Verify(r => r.BookRepository.AddBook(It.IsAny<Book>()), Times.Once);
    }
}
