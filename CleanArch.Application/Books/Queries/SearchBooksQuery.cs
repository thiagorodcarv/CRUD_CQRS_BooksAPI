using CleanArch.Domain.Abstractions;
using CleanArch.Domain.DTOs;
using MediatR;


namespace CleanArch.Application.Books.Queries;

public class SearchBooksQuery : IRequest<PagedResult<BookDto>>
{
    public String Query { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, PagedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task<PagedResult<BookDto>> IRequestHandler<SearchBooksQuery, PagedResult<BookDto>>.Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var resultado = await _unitOfWork.BookRepository.SearchByString(request.Query, request.Page, request.PageSize);

            return resultado;
        }
    }
}
