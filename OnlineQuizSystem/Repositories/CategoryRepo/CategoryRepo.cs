using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories.CategoryRepo;

public class CategoryRepo(AppDbContext _context) : ICategoryRepo
{
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.Include(c => c.Questions).ToListAsync();
    }
    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }
    public async Task<Category> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
        
    }
    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }
    
    
}