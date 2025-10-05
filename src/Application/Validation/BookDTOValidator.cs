using Application.DTOs;
using Domain.Enums.ReadingJournal;
using FluentValidation;

namespace Application.Validation
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Authors).NotEmpty();
            RuleForEach(x => x.Authors)
                .SetValidator(new AuthorDTOValidator());
            RuleFor(x => x.Publisher).NotEmpty();
            RuleFor(x => x.Status).NotEmpty().IsInEnum();
            RuleFor(x => x.Genres)
                .NotEmpty()
                .ForEach(genreRule => genreRule.IsInEnum());
            RuleFor(x => x.Format).NotEmpty().IsInEnum();
            RuleFor(x => x.Pages).NotEmpty().GreaterThanOrEqualTo(1);

            RuleFor(x => x.CurrentPage.Value)
                .GreaterThan(0).WithMessage("CurrentPage deve ser maior que 0.")
                .LessThanOrEqualTo(x => x.Pages).WithMessage("CurrentPage deve ser menor ou igual que Pages.")
                .When(x => x.CurrentPage.HasValue);
            RuleFor(x => x.Price)
                .Must(price => price == null || decimal.Round(price.Value, 2) == price.Value)
                .WithMessage("O preço deve ter no máximo duas casas decimais.");

            RuleFor(x => x.EndDate)
                .Must((dto, endDate) => !endDate.HasValue || dto.StartDate.HasValue)
                .WithMessage("A data final só pode ser informada se a data inicial estiver preenchida.");
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("A data final deve ser maior ou igual à data inicial.");

            RuleFor(x => x).Custom((dto, context) =>
            {
                // 1) Se status == Finished, então EndDate deve estar preenchida e CurrentPage == Pages
                if (dto.Status == Status.Finished)
                {
                    if (!dto.EndDate.HasValue)
                        context.AddFailure(nameof(dto.EndDate), "EndDate deve estar preenchida quando o status for Finished.");

                    if (dto.CurrentPage != dto.Pages)
                        context.AddFailure(nameof(dto.CurrentPage), "Current Page deve ser igual a Pages quando o status for Finished.");
                }

                // 2) Se CurrentPage == Pages, então status deve ser Finished e EndDate preenchida
                if (dto.CurrentPage == dto.Pages)
                {
                    if (dto.Status != Status.Finished)
                        context.AddFailure(nameof(dto.Status), "Status deve ser Finished quando CurrentPage == Pages.");

                    if (!dto.EndDate.HasValue)
                        context.AddFailure(nameof(dto.EndDate), "EndDate deve estar preenchida quando CurrentPage == Pages.");
                }

                // 3) Se EndDate estiver preenchida, então status deve ser Finished e CurrentPage == Pages
                if (dto.EndDate.HasValue)
                {
                    if (dto.Status != Status.Finished)
                        context.AddFailure(nameof(dto.Status), "Status deve ser Finished quando EndDate estiver preenchida.");

                    if (dto.CurrentPage != dto.Pages)
                        context.AddFailure(nameof(dto.CurrentPage), "CurrentPage deve ser igual a Pages quando EndDate estiver preenchida.");
                }
            });
        }
    }
}
