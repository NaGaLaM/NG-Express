using NG_Express.Models;

namespace NG_Express.Services.Products
{
    public interface IProductService
    {
        
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetDiscountedProductsAsync();
        Task<Product> GetProductByIdAsync(int Id);
        Task<List<Product>> GetProductsByCategoryId(int Id);
    }
}
