using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Genrers.Queries;

public class GetGenrersQuery : IRequest<IEnumerable<Genrer>>
{
    public class GetGenrersQueryHandler : IRequestHandler<GetGenrersQuery, IEnumerable<Genrer>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGenrersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Genrer>> Handle(GetGenrersQuery request, CancellationToken cancellationToken)
        {
            var genrers = await _unitOfWork.GenrerRepository.GetGenrers();
            return genrers;
        }
    }
}

