namespace OnlineQuizSystem.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly Repositories.CategoryRepo.ICategoryRepo _categoryRepo;

    public CategoryService(Repositories.CategoryRepo.ICategoryRepo categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<Models.Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepo.GetAllCategoriesAsync();
    }

    public async Task<Models.Category?> GetCategoryByIdAsync(string id)
    {
        return await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(id));
    }

    public async Task<Models.Category> AddCategoryAsync(DTOs.CategoryDTOs.CategoryDTO categoryDto)
    {
        var category = new Models.Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };
        return await _categoryRepo.AddCategoryAsync(category);
    }

    public async Task<Models.Category?> UpdateCategoryAsync(string id, DTOs.CategoryDTOs.CategoryDTO categoryDto)
    {
        var existingCategory = await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(id));
        if (existingCategory == null)
            return null;

        existingCategory.Name = categoryDto.Name;
        existingCategory.Description = categoryDto.Description;

        return await _categoryRepo.UpdateCategoryAsync(existingCategory);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categoryRepo.DeleteCategoryAsync(Guid.Parse(id));
    }
    
}