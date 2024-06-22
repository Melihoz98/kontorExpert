using System.Collections.Generic;
using System.Threading.Tasks;
using kontorExpert.Models;

public interface ICategoryAccess
{
    Task AddCategoryAsync(Category category);
    Task<Category> GetCategoryByIDAsync(int categoryId);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int categoryId);
    Task<List<Category>> GetAllCategoriesAsync();
}
