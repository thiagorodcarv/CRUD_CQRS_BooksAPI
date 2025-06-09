using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infrastructure.EntityConfiguration;

public class GenrerConfiguration : IEntityTypeConfiguration<Genrer>
{
    public void Configure(EntityTypeBuilder<Genrer> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).HasMaxLength(50).IsRequired();

        builder.HasData(
            new Genrer(1, "Romance"),
            new Genrer(2, "Ficção Científica"),
            new Genrer(3, "Fantasia"),
            new Genrer(4, "Terror"),
            new Genrer(5, "Mistério"),
            new Genrer(6, "Aventura"),
            new Genrer(7, "Biografia"),
            new Genrer(8, "Drama"),
            new Genrer(9, "Distopia"),
            new Genrer(10, "Histórico")
        );
    }
}
