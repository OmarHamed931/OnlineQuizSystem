using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.CategoryService;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTOs.CategoryDTO>> GetAllCategoriesAsync();
    Task<Models.Category?> GetCategoryByIdAsync(string id);
    Task<Models.Category> AddCategoryAsync(DTOs.CategoryDTOs.CreateCategoryDTO categoryDto);
    Task<Models.Category?> UpdateCategoryAsync(string id, DTOs.CategoryDTOs.CategoryDTO categoryDto);
    Task DeleteCategoryAsync(string id);
    
}