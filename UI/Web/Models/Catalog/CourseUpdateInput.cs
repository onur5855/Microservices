using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        [Display(Name = "Kurs İsmi")]
        public string Name { get; set; }

        [Display(Name = "Kurs Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Kurs Fiyat")]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string? Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs Kategori")]
        public string CategorId { get; set; }
        [Display(Name = "Kurs Resmi")]
        public IFormFile formFile { get; set; }
    }
}
