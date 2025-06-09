using CleanArch.Application.Genrers.Commands;
using CleanArch.Application.Genrers.Queries;
using CleanArch.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("[controller]")]
[ApiController]
public class GenrersController : ControllerBase
{
    private readonly IMediator _mediator;
    public GenrersController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenrers()
    {
        var query = new GetGenrersQuery();
        var genrers = await _mediator.Send(query);
        return Ok(genrers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenrer(int id)
    {
        var query = new GetGenrerByIdQuery { Id = id };
        var genrer = await _mediator.Send(query);

        return genrer != null ? Ok(genrer) : NotFound("Genrer not found.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenrer(CreateGenrerCommand command)
    {
        var createdGenrer = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetGenrer), new { id = createdGenrer.Id }, createdGenrer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenrer(int id, UpdateGenrerCommand command)
    {
        command.Id = id;
        var updatedGenrer = await _mediator.Send(command);

        return updatedGenrer != null ? Ok(updatedGenrer) : NotFound("Genrer not found.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenrer(int id)
    {
        var command = new DeleteGenrerCommand { Id = id };
        var deletedGenrer = await _mediator.Send(command);

        return deletedGenrer != null ? Ok(deletedGenrer) : NotFound("Genrer not found.");
    }
}
