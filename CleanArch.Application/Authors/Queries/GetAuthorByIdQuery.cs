using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<Author>
{
    public int Id { get; set; }

    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorById(request.Id);
            return author;
        }
    }
}