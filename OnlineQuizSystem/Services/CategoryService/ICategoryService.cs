using OnlineQuizSystem.DTOs;


namespace OnlineQuizSystem.Services.CategoryService;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTOs.CategoryDTO>> GetAllCategoriesAsync();
    Task<Models.Category?> GetCategoryByIdAsync(string id);
    Task<Models.Category> AddCategoryAsync(CategoryDTOs.CreateCategoryDTO categoryDto);
    Task<Models.Category?> UpdateCategoryAsync(string id, CategoryDTOs.UpdateDTO updateDto);
    Task DeleteCategoryAsync(string id);
    
}