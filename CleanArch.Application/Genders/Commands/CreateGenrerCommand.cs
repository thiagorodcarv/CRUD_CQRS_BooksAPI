using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;
namespace CleanArch.Application.Genrers.Commands;
public class CreateGenrerCommand : GenrerCommandBase
{
    public class CreateGenrerCommandHandler : IRequestHandler<CreateGenrerCommand, Genrer>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateGenrerCommand> _validator;
        private readonly IMediator _mediator;
        public CreateGenrerCommandHandler(IUnitOfWork unitOfWork,
                                          IValidator<CreateGenrerCommand> validator,
                                          IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mediator = mediator;
        }
        public async Task<Genrer> Handle(CreateGenrerCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var newGenrer = new Genrer(request.Name);

            await _unitOfWork.GenrerRepository.AddGenrer(newGenrer);
            await _unitOfWork.CommitAsync();

            return newGenrer;
        }
    }

}
