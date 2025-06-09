using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Books.Queries;

public class GetBooksQuery : IRequest<IEnumerable<Book>>
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.BookRepository.GetBooks();
            return books;
        }
    }
}

