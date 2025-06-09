using Moq;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Abstractions;
using CleanArch.Application.Authors.Commands;
using static CleanArch.Application.Authors.Commands.CreateAuthorCommand;
using FluentValidation;
using MediatR;

public class CreateAuthorHandlerTests
{
    [Fact]
    public async Task Should_Create_Author_With_Valide_Name()
    {
        var repoMock = new Mock<IUnitOfWork>();
        var validatorMock = new Mock<IValidator<CreateAuthorCommand>>();
        var mediatorMock = new Mock<IMediator>();

        var handler = new CreateAuthorCommandHandler(repoMock.Object,validatorMock.Object,mediatorMock.Object);

        var command = new CreateAuthorCommand { Name = "Machado de Assis" };

        var result = await handler.Handle(command, CancellationToken.None);

        repoMock.Verify(r => r.AuthorRepository.AddAuthor(It.IsAny<Author>()), Times.Once);
    }
}
