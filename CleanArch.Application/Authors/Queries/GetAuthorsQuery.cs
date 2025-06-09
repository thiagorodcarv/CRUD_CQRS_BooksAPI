using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Authors.Queries;

public class GetAuthorsQuery : IRequest<IEnumerable<Author>>
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.AuthorRepository.GetAuthors();
            return authors;
        }
    }
}

