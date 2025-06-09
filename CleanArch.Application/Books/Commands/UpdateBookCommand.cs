using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Books.Commands;

public sealed class UpdateBookCommand : BookCommandBase
{
    public int Id { get; set; }
    public class UpdateBookCommandHandler :
                 IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _unitOfWork.BookRepository.GetBookById(request.Id);

            if (existingBook is null)
                throw new InvalidOperationException("Book not found");

            existingBook.Update(request.Title, request.AuthorId, request.GenrerId);
            _unitOfWork.BookRepository.UpdateBook(existingBook);
            await _unitOfWork.CommitAsync();

            return existingBook;
        }
    }
}
