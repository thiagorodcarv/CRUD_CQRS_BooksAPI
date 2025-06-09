using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Authors.Commands;
public sealed class DeleteAuthorCommand : IRequest<Author>
{
    public int Id { get; set; }

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> Handle(DeleteAuthorCommand request,
                     CancellationToken cancellationToken)
        {
            var deletedAuthor = await _unitOfWork.AuthorRepository.DeleteAuthor(request.Id);

            if (deletedAuthor is null)
                throw new InvalidOperationException("Author not found");

            await _unitOfWork.CommitAsync();
            return deletedAuthor;
        }
    }
}
