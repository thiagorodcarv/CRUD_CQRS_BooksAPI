using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
namespace CleanArch.Application.Books.Commands;
public class CreateBookCommand : BookCommandBase
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateBookCommand> _validator;
        private readonly IMediator _mediator;
        public CreateBookCommandHandler(IUnitOfWork unitOfWork,
                                          IValidator<CreateBookCommand> validator,
                                          IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediator = mediator;
        }
        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var newBook = new Book(request.Title, request.AuthorId, request.GenrerId);

            await _unitOfWork.BookRepository.AddBook(newBook);
            await _unitOfWork.CommitAsync();

            return newBook;
        }
    }

}
