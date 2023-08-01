using FluentValidation;
using Web.Models.Discounts;

namespace Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(a=>a.Code).NotEmpty().WithMessage("Lütfen İndirim Kodunu giriniz");
        }
    }
}
