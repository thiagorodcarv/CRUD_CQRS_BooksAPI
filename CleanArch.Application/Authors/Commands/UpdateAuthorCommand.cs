using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Authors.Commands;

public sealed class UpdateAuthorCommand : AuthorCommandBase
{
    public int Id { get; set; }
    public class UpdateAuthorCommandHandler :
                 IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var existingAuthor = await _unitOfWork.AuthorRepository.GetAuthorById(request.Id);

            if (existingAuthor is null)
                throw new InvalidOperationException("Author not found");

            existingAuthor.Update(request.Name);
            _unitOfWork.AuthorRepository.UpdateAuthor(existingAuthor);
            await _unitOfWork.CommitAsync();

            return existingAuthor;
        }
    }
}
