using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Genrers.Queries;

public class GetGenrerByIdQuery : IRequest<Genrer>
{
    public int Id { get; set; }

    public class GetGenrerByIdQueryHandler : IRequestHandler<GetGenrerByIdQuery, Genrer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGenrerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Genrer> Handle(GetGenrerByIdQuery request, CancellationToken cancellationToken)
        {
            var genrer = await _unitOfWork.GenrerRepository.GetGenrerById(request.Id);
            return genrer;
        }
    }
}