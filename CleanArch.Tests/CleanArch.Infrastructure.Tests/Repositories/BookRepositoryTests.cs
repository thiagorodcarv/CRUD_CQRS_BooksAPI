using Xunit;
using Microsoft.EntityFrameworkCore;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repositories;
using CleanArch.Domain.Entities;

public class BookRepositoryTests
{
    [Fact]
    public async Task Shoul_Add_A_Book()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new AppDbContext(options);
        var repo = new BookRepository(context);

        var livro = new Book("Book Test", 1, 1);
        await repo.AddBook(livro);

        Assert.Single(await context.Books.ToListAsync());
    }
}
