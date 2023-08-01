using FluentValidation;
using Web.Models.Catalog;

namespace Web.Validators
{
    public class CourseUpdateInputValidator : AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Bu İsim Alanı Boş Olamaz");
            RuleFor(a => a.Description).NotEmpty().WithMessage("Bu Açıklama Alanı Boş Olamaz");
            RuleFor(a => a.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre Alanı Boş Olamaz");
            //$$$$,$$
            RuleFor(a => a.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Olamaz").ScalePrecision(2,6).WithMessage("Hatalı Format");
        }
    }
}
