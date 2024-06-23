using Data;
using Microsoft.EntityFrameworkCore;
using NG_Express.Models;
namespace Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext DB;
        public CategoryService(AppDbContext db)
        {
            DB = db;
        }
        public async Task<List<Category>> GetCategories()
        {
            List<Category> Categories;
            Categories = await DB.Categories.ToListAsync();
            return Categories;
        }
    }
}
