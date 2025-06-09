using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genrer> Genrers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthorConfiguration());
        builder.ApplyConfiguration(new GenrerConfiguration());
        builder.ApplyConfiguration(new BookConfiguration());
    }
}
