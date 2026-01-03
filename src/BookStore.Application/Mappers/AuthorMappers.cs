using BookStore.Contracts.Applications.Dto.Authors;
using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class AuthorMappers
{
    public static Author ToModel(this CreateAuthorRequestDto requestDto) =>
        new Author
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Biography = requestDto.Biography,
            Birthday = requestDto.Birthday,
            Nationality = requestDto.Nationality,
        };

    public static AuthorResponseDto ToResponse(this Author author) => 
        new AuthorResponseDto(
            author.Id,
            author.FirstName,
            author.LastName,
            author.Biography,
            author.Birthday,
            author.Nationality
        );

    public static ICollection<AuthorResponseDto> ToResponse(this ICollection<Author> authors) =>
        authors.Select(ToResponse).ToList();

    public static void Update(this Author author, CreateAuthorRequestDto requestDto)
    {
        author.FirstName = requestDto.FirstName;
        author.LastName = requestDto.LastName;
        author.Biography = requestDto.Biography;
        author.Birthday = requestDto.Birthday;
        author.Nationality = requestDto.Nationality;
    }
}
