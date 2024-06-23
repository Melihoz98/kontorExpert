using System.Collections.Generic;
using System.Threading.Tasks;
using kontorExpert.Models;

public class ParentCategoryLogic
{
    private readonly IParentCategoryAccess _parentCategoryAccess;

    public ParentCategoryLogic(IParentCategoryAccess parentCategoryAccess)
    {
        _parentCategoryAccess = parentCategoryAccess;
    }

    public async Task AddParentCategoryAsync(ParentCategory parentCategory)
    {
        await _parentCategoryAccess.AddParentCategoryAsync(parentCategory);
    }

    public async Task<ParentCategory> GetParentCategoryByIDAsync(int parentCategoryId)
    {
        return await _parentCategoryAccess.GetParentCategoryByIDAsync(parentCategoryId);
    }

    public async Task UpdateParentCategoryAsync(ParentCategory parentCategory)
    {
        await _parentCategoryAccess.UpdateParentCategoryAsync(parentCategory);
    }

    public async Task DeleteParentCategoryAsync(int parentCategoryId)
    {
        await _parentCategoryAccess.DeleteParentCategoryAsync(parentCategoryId);
    }

    public async Task<List<ParentCategory>> GetAllParentCategoriesAsync()
    {
        return await _parentCategoryAccess.GetAllParentCategoriesAsync();
    }
}
