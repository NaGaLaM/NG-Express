namespace NG_Express.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string? SecondAddress { get; set; }
        public string? Phone { get; set; }
        public List<Product> Products { get; set; }
    }
}
