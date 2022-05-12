using basketbackend.Misc;
using basketbackend.Presentation.Models;
using FluentValidation;

namespace basketbackend.Presentation.Validators
{
    public class AddItemToBasketModelValidator : AbstractValidator<AddItemToBasketModel>
    {
        public AddItemToBasketModelValidator()
        {
            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage(Constants.PricePositiveMessage);
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(Constants.NameNotEmptyMessage);
            RuleFor(x => x.Name).Matches(Constants.AlphanumericRegex).WithMessage(Constants.NameAlphanumericalMessage);
        }
    }
}
