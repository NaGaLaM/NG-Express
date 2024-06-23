using Data;
using Microsoft.EntityFrameworkCore;
using NG_Express.Models;
namespace NG_Express.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext DB;
        public ProductService(AppDbContext db)
        {
            this.DB = db;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products;
            products = await DB.Products
                                .Include(p=>p.Category)
                                .OrderBy(p=>p.Id)
                            .ToListAsync();
            return products;

        }
    }
}
