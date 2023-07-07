using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Services.Catalog.Model;

namespace Services.Catalog.Dtos
{
    public class CourseDto
    {
     
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategorId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
