using OnlineQuizSystem.Repositories.CategoryRepo;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.DTOs;

namespace OnlineQuizSystem.Services.CategoryService;

public class CategoryService : ICategoryService
{
    // note: reference cycle between category and question results in a worse JSON response payload
    // possible solutions: Using [JsonIgnore] attribute on navigation properties partially solves the problem, but not completely
    // Another solution is to use DTOs to shape the response data
    private readonly ICategoryRepo _categoryRepo;

    public CategoryService(ICategoryRepo categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<CategoryDTOs.CategoryDTO>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepo.GetAllCategoriesAsync();
        return categories.Select(c => new CategoryDTOs.CategoryDTO
        {    Id = c.Id,
            Name = c.Name,
            Description = c.Description,
                        NumberOfQuestions = c.Questions != null ? c.Questions.Count : 0

        }).ToList();


    }

    public async Task<Category?> GetCategoryByIdAsync(string id)
    {
        return await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(id));
    }

    public async Task<Category> AddCategoryAsync(CategoryDTOs.CreateCategoryDTO categoryDto)
    {
        var existingCategory = await _categoryRepo.GetCategoryByNameAsync(categoryDto.Name);
        if (existingCategory != null)
            throw new Exception("Category with the same name already exists.");
        var category = new Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };
        return await _categoryRepo.AddCategoryAsync(category);
    }

    public async Task<Category?> UpdateCategoryAsync(string id, CategoryDTOs.CategoryDTO categoryDto)
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