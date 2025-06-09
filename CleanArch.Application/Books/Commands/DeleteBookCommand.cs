using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Books.Commands;
public sealed class DeleteBookCommand : IRequest<Book>
{
    public int Id { get; set; }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(DeleteBookCommand request,
                     CancellationToken cancellationToken)
        {
            var deletedBook = await _unitOfWork.BookRepository.DeleteBook(request.Id);

            if (deletedBook is null)
                throw new InvalidOperationException("Book not found");

            await _unitOfWork.CommitAsync();
            return deletedBook;
        }
    }
}
