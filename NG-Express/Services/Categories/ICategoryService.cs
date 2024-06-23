using NG_Express.Models;
namespace Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}
