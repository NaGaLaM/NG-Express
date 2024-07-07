using System.ComponentModel.DataAnnotations.Schema;

namespace NG_Express.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string Image {  get; set; }
        public bool isMain { get; set; }
        public DateOnly? Created {  get; set; }
        public DateOnly? Updated {  get; set; }
        public DateOnly? Deleted {  get; set; }
        public Product Product { get; set; }
    }
}
