using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Genrers.Commands;
public sealed class DeleteGenrerCommand : IRequest<Genrer>
{
    public int Id { get; set; }

    public class DeleteGenrerCommandHandler : IRequestHandler<DeleteGenrerCommand, Genrer>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGenrerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Genrer> Handle(DeleteGenrerCommand request,
                     CancellationToken cancellationToken)
        {
            var deletedGenrer = await _unitOfWork.GenrerRepository.DeleteGenrer(request.Id);

            if (deletedGenrer is null)
                throw new InvalidOperationException("Genrer not found");

            await _unitOfWork.CommitAsync();
            return deletedGenrer;
        }
    }
}
