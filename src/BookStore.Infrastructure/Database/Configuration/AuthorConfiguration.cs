using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Database.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FirstName)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(a => a.Biography)
            .HasMaxLength(512)
            .IsRequired();

        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
    }
}
