namespace OnlineQuizSystem.Services.CategoryService;

public interface ICategoryService
{
    Task<IEnumerable<Models.Category>> GetAllCategoriesAsync();
    Task<Models.Category?> GetCategoryByIdAsync(string id);
    Task<Models.Category> AddCategoryAsync(DTOs.CategoryDTOs.CategoryDTO categoryDto);
    Task<Models.Category?> UpdateCategoryAsync(string id, DTOs.CategoryDTOs.CategoryDTO categoryDto);
    Task DeleteCategoryAsync(string id);
    
}