using Application.DTOs;
using FluentValidation;

namespace Application.Validation
{
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
