using BookStore.Contracts.Rest.v1.Authors;
using BookStore.Domain.Models;

namespace BookStore.API.Mappers;

public static class AuthorMappers
{
    public static Author ToModel(this CreateAuthorDto requestDto)
    {
        return new Author
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Biography = requestDto.Biography,
            Birthday = requestDto.Birthday,
            Nationality = requestDto.Nationality,
        };
    }

    public static AuthorDto ToResponse(this Author author)
    {
        return new AuthorDto(
            author.Id,
            author.FirstName,
            author.LastName,
            author.Biography,
            author.Birthday,
            author.Nationality
        );
    }
}
