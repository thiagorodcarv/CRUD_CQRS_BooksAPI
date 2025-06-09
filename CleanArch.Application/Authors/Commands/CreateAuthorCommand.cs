using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
namespace CleanArch.Application.Authors.Commands;
public class CreateAuthorCommand : AuthorCommandBase
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateAuthorCommand> _validator;
        private readonly IMediator _mediator;
        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork,
                                          IValidator<CreateAuthorCommand> validator,
                                          IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediator = mediator;
        }
        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var newAuthor = new Author(request.Name);

            await _unitOfWork.AuthorRepository.AddAuthor(newAuthor);
            await _unitOfWork.CommitAsync();

            return newAuthor;
        }
    }

}
