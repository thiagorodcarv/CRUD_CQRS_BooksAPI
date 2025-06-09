using CleanArch.Domain.Abstractions;
using FluentValidation;
namespace CleanArch.Application.Genrers.Commands.Validations;
public class CreateGenrerCommandValidator : AbstractValidator<CreateGenrerCommand>
{
    public CreateGenrerCommandValidator(IUnitOfWork unitOfWork)
    {
       RuleFor(c => c.Name)
         .NotEmpty().WithMessage("Please ensure you have entered the Name of the Genrer")
         .Length(4, 250).WithMessage("The Genrer must have between 4 and 50 characters");
    }
}
