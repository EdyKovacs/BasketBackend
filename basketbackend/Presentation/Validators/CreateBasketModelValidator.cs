using basketbackend.Misc;
using basketbackend.Presentation.Models;
using FluentValidation;

namespace basketbackend.Presentation.Validators
{
    public class CreateBasketModelValidator : AbstractValidator<CreateBasketModel>
    {
        public CreateBasketModelValidator()
        {
            RuleFor(x => x.Customer).NotEmpty().NotNull().WithMessage(Constants.NameNotEmptyMessage);
            RuleFor(x => x.Customer).Matches(Constants.AlphanumericRegex).WithMessage(Constants.NameAlphanumericalMessage);
        }
    }
}
