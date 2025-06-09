using CleanArch.Application.Books.Commands;
using CleanArch.Application.Books.Queries;
using CleanArch.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string querySearch, int page = 1, int pageSize = 20)
    {
        var query = new SearchBooksQuery { Query = querySearch, Page = page, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Retorna livros paginados com ordenação por título, autor ou gênero.
    /// </summary>
    /// <param name="orderBy">Campo para ordenação: Titulo, Autor, Genero</param>
    /// <param name="direcao">Direção da ordenação: asc ou desc</param>
    [HttpGet("list")]
    public async Task<IActionResult> GetAllBooksPaged([FromQuery] int page = 1, int pageSize = 20,
    string? ordedBy = null, string? direction = null)
    {
        var query = new ListBooksPagedQuery { Direction = direction, OrdedBy = ordedBy, Page = page, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var query = new GetBooksQuery();
        var books = await _mediator.Send(query);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var query = new GetBookByIdQuery { Id = id };
        var book = await _mediator.Send(query);

        return book != null ? Ok(book) : NotFound("Book not found.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookCommand command)
    {
        var createdBook = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookCommand command)
    {
        command.Id = id;
        var updatedBook = await _mediator.Send(command);

        return updatedBook != null ? Ok(updatedBook) : NotFound("Book not found.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var command = new DeleteBookCommand { Id = id };
        var deletedBook = await _mediator.Send(command);

        return deletedBook != null ? Ok(deletedBook) : NotFound("Book not found.");
    }
}
