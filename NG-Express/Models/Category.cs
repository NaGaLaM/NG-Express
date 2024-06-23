namespace NG_Express.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly? Created { get; set; }
        public DateOnly? Updated { get; set; }
        public DateOnly? Deleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
