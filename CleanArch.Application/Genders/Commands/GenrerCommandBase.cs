using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Genrers.Commands;

public abstract class GenrerCommandBase : IRequest<Genrer>
{
    public string? Name { get; set; }
}
