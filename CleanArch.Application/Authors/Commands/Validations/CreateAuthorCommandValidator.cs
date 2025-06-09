using CleanArch.Domain.Abstractions;
using FluentValidation;
namespace CleanArch.Application.Authors.Commands.Validations;
public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator(IUnitOfWork unitOfWork)
    {
       RuleFor(c => c.Name)
         .NotEmpty().WithMessage("Please ensure you have entered the Author's Name")
         .Length(4, 250).WithMessage("The Name must have between 4 and 250 characters");

    }
}
