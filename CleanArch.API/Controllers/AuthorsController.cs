using CleanArch.Application.Authors.Commands;
using CleanArch.Application.Authors.Queries;
using CleanArch.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthorsController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var query = new GetAuthorsQuery();
        var authors = await _mediator.Send(query);
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var query = new GetAuthorByIdQuery { Id = id };
        var author = await _mediator.Send(query);

        return author != null ? Ok(author) : NotFound("Author not found.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
    {
        var createdAuthor = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorCommand command)
    {
        command.Id = id;
        var updatedAuthor = await _mediator.Send(command);

        return updatedAuthor != null ? Ok(updatedAuthor) : NotFound("Author not found.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var command = new DeleteAuthorCommand { Id = id };
        var deletedAuthor = await _mediator.Send(command);

        return deletedAuthor != null ? Ok(deletedAuthor) : NotFound("Author not found.");
    }
}
