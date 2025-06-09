using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Authors.Commands;

public abstract class AuthorCommandBase : IRequest<Author>
{
    public string? Name { get; set; }
}
