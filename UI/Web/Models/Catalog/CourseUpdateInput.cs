using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        [Display(Name = "Kurs İsmi")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Kurs Açıklama")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Kurs Fiyat")]
        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string? Picture { get; set; }
        public FeatureViewModel Feature { get; set; }
        [Display(Name = "Kurs Kategori")]
        [Required]
        public string CategorId { get; set; }
        [Display(Name = "Kurs Resmi")]
        public IFormFile formFile { get; set; }
    }
}
