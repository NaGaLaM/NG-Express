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
                                .OrderBy(p => EF.Functions.Random())
                                .ToListAsync(); 
            return products;

        }
        public async Task<List<Product>> GetProductsByCategoryId(int Id)
        {
            List<Product> products ;
                products = await DB.Products
                                    .Include(p=>p.Category)
                                    .Where(p=>p.CategoryId==Id)
                                    .OrderBy (p => EF.Functions.Random())
                                    .ToListAsync();
            return products;

        }
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var product = await DB.Products.FirstOrDefaultAsync(a=>a.Id == Id);
            return product;
        }
        public async Task<List<Product>> GetDiscountedProductsAsync()
        {
            List<Product> products;
            products = await DB.Products
                        .Include(p => p.Category)
                        .Where(p => p.Discount != null)
                        .OrderBy(p => EF.Functions.Random())
                        .ToListAsync();
            return products;
        }
    }
}
