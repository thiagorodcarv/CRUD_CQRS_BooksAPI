using Moq;
using FluentValidation.TestHelper;
using CleanArch.Application.Books.Commands;
using CleanArch.Domain.Abstractions;
using CleanArch.Application.Books.Commands.Validations;

public class CreateBookCommandValidatorTests
{
    [Fact]
    public async Task Should_Return_Error_If_Title_Is_Empty()
    {
        var repoMock = new Mock<IUnitOfWork>();

        var validator = new CreateBookCommandValidator(repoMock.Object);

        var command = new CreateBookCommand
        {
            Title = "",
            AuthorId = 1,
            GenrerId = 1
        };

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(c => c.Title);
    }
}
