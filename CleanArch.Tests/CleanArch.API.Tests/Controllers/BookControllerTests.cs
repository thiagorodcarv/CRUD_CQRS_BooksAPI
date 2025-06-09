using Moq;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Abstractions;
using CleanArch.API.Controllers;
using MediatR;

public class BookControllerTests
{
    [Fact]
    public async Task Should_Return_OK_With_Books()
    {
        var service = new Mock<IUnitOfWork>();
        var mediator = new Mock<IMediator>();

        service.Setup(s => s.BookRepository.GetBooks()).ReturnsAsync(new List<Book>
        {
            new Book ("Livro A", 1, 1 )
        });

        var controller = new BooksController(mediator.Object, service.Object);
        var result = await controller.GetBooks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
