using System.Collections.Generic;
using System.Threading.Tasks;
using kontorExpert.Models;

public interface IParentCategoryAccess
{
    Task AddParentCategoryAsync(ParentCategory parentCategory);
    Task<ParentCategory> GetParentCategoryByIDAsync(int parentCategoryId);
    Task UpdateParentCategoryAsync(ParentCategory parentCategory);
    Task DeleteParentCategoryAsync(int parentCategoryId);
    Task<List<ParentCategory>> GetAllParentCategoriesAsync();
}
