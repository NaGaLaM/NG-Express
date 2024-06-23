using NG_Express.Models;

namespace NG_Express.Services.Products
{
    public interface IProductService
    {
        
        Task<List<Product>> GetAllProductsAsync();
    }
}
