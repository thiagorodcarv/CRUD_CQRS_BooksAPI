using CleanArch.Domain.Abstractions;
using FluentValidation;
namespace CleanArch.Application.Books.Commands.Validations;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    private readonly IUnitOfWork unitOfWork;
    public CreateBookCommandValidator(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;

        RuleFor(c => c.Title)
         .NotEmpty().WithMessage("Please ensure you have entered the Title")
         .Length(4, 250).WithMessage("The Title must have between 4 and 250 characters");

        RuleFor(c => c.AuthorId)
            .NotNull().WithMessage("Author must be informed")
            .GreaterThan(0).WithMessage("Author must be a valid id")
            .MustAsync(async (authorId, cancellationToken) =>
            {
                if (!authorId.HasValue || authorId.Value <= 0)
                    return false;

                var result = await unitOfWork.AuthorRepository.GetAuthorById(authorId.Value);
                return result != null;
            }).WithMessage("The author id must exist");

        RuleFor(c => c.GenrerId)
            .NotNull().WithMessage("Genrer must be informed")
            .GreaterThan(0).WithMessage("Genrer must be a valid id")
            .MustAsync(async (genrerId, cancellationToken) =>
            {
                if (!genrerId.HasValue || genrerId.Value <= 0)
                    return false;

                var result = await unitOfWork.GenrerRepository.GetGenrerById(genrerId.Value);
                return result != null;
            }).WithMessage("The genrer id must exist");

    }
}
