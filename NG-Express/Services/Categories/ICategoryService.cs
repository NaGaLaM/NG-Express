using NG_Express.Models;
namespace NG_Express.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}
