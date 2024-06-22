using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kontorExpert.Models;

public class CategoryLogic
{
    private readonly ICategoryAccess _categoryAccess;

    public CategoryLogic(ICategoryAccess categoryAccess)
    {
        _categoryAccess = categoryAccess ?? throw new ArgumentNullException(nameof(categoryAccess));
    }

    public async Task AddCategoryAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (string.IsNullOrWhiteSpace(category.CategoryName))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }

        // Add additional business logic if needed

        await _categoryAccess.AddCategoryAsync(category);
    }

    public async Task<Category> GetCategoryByIDAsync(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("Category ID must be greater than zero.");
        }

        var category = await _categoryAccess.GetCategoryByIDAsync(categoryId);

        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }

        return category;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (string.IsNullOrWhiteSpace(category.CategoryName))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }

        // Add additional business logic if needed

        await _categoryAccess.UpdateCategoryAsync(category);
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("Category ID must be greater than zero.");
        }

        var category = await _categoryAccess.GetCategoryByIDAsync(categoryId);

        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }

        await _categoryAccess.DeleteCategoryAsync(categoryId);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await _categoryAccess.GetAllCategoriesAsync();

        if (categories == null || categories.Count == 0)
        {
            throw new KeyNotFoundException("No categories found.");
        }

        return categories;
    }
}
