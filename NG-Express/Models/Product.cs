using System.ComponentModel.DataAnnotations.Schema;

namespace NG_Express.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public string Image { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Category Category { get; set; }
    }
}
