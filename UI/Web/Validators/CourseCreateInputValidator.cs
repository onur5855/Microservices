using FluentValidation;
using Web.Models.Catalog;

namespace Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            //.net 6 ve sonrasında propertiler null bırakılabilir işaretlemedikçe required gibi davranır  propertide '?' yoksa buradaki
            //fieldler not empdeki mesajlar gözükmez

            RuleFor(a => a.Name).NotEmpty().WithMessage("Bu İsim Alanı Boş Olamaz");
            RuleFor(a => a.Description).NotEmpty().WithMessage("Bu Açıklama Alanı Boş Olamaz");
            RuleFor(a => a.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre Alanı Boş Olamaz");
            //$$$$,$$
            RuleFor(a => a.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Olamaz").ScalePrecision(2,6).WithMessage("Hatalı Format");
            RuleFor(a => a.CategorId).NotEmpty().WithMessage("Categori Alanı Seçiniz");
        }
    }
}
