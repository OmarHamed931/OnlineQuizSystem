namespace OnlineQuizSystem.Repositories.CategoryRepo;
using OnlineQuizSystem.Models;
public interface ICategoryRepo
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category> AddCategoryAsync(Models.Category category);
    Task<Category> UpdateCategoryAsync(Models.Category category);
    Task DeleteCategoryAsync(Guid id);
    Task<Category?> GetCategoryByNameAsync(string name);
    
}