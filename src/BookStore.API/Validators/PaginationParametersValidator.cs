using BookStore.Application.Abstractions.Database.Models;
using FluentValidation;

namespace BookStore.API.Validators;

public class PaginationParametersValidator : AbstractValidator<PaginationParameters>
{
    public PaginationParametersValidator()
    {
        RuleFor(p => p.Take)
            .GreaterThan(0)
            .NotEmpty();

        RuleFor(p => p.Page)
            .GreaterThan(0);
    }
}
