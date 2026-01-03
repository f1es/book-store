using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookStore.Infrastructure.Database;

public class BookStoreDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
