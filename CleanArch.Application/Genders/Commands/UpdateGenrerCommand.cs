using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Genrers.Commands;

public sealed class UpdateGenrerCommand : GenrerCommandBase
{
    public int Id { get; set; }
    public class UpdateGenrerCommandHandler :
                 IRequestHandler<UpdateGenrerCommand, Genrer>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateGenrerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Genrer> Handle(UpdateGenrerCommand request, CancellationToken cancellationToken)
        {
            var existingGenrer = await _unitOfWork.GenrerRepository.GetGenrerById(request.Id);

            if (existingGenrer is null)
                throw new InvalidOperationException("Genrer not found");

            existingGenrer.Update(request.Name);
            _unitOfWork.GenrerRepository.UpdateGenrer(existingGenrer);
            await _unitOfWork.CommitAsync();

            return existingGenrer;
        }
    }
}
