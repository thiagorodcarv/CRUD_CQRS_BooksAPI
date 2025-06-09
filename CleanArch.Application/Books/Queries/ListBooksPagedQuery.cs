using CleanArch.Domain.Abstractions;
using CleanArch.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Books.Queries;

public class ListBooksPagedQuery : IRequest<PagedResult<BookDto>>
{
    public String OrdedBy { get; set; }
    public String Direction { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public class ListBooksPagedQueryHandler : IRequestHandler<ListBooksPagedQuery, PagedResult<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListBooksPagedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<BookDto>> Handle(ListBooksPagedQuery request, CancellationToken cancellationToken)
        {
            var resultado = await _unitOfWork.BookRepository.GetAllPaged(request.Page, request.PageSize, request.OrdedBy, request.Direction);

            return resultado;
        }
    }
}
