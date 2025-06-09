using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infrastructure.EntityConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(250).IsRequired();
        builder.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId);
//            .HasPrincipalKey(x => x.Id); The EF already get the column since the entity is already configured
        builder.HasOne<Genrer>().WithMany().HasForeignKey(y => y.GenrerId);

        builder.HasData(
        );
    }
}
