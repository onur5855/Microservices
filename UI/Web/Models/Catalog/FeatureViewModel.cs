using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs Süre")]
        public int Duration { get; set; }
    }
}
