using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Database.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Title)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(b => b.Price)
            .HasPrecision(18, 2);

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        builder.HasOne(b => b.Publisher)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.PublisherId);
    }
}
