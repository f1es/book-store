using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class AuthorMappers
{
    public static void Update(this Author existingAuthor, Author author)
    {
        existingAuthor.FirstName = author.FirstName;
        existingAuthor.LastName = author.LastName;
        existingAuthor.Biography = author.Biography;
        existingAuthor.Birthday = author.Birthday;
        existingAuthor.Nationality = author.Nationality;
    }
}
