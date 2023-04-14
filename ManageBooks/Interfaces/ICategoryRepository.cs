using Shared.Models;

namespace ManageBooks.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
    }
}
