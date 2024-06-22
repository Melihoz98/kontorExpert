using System.Collections.Generic;
using kontorExpert.Models;

public interface ICategoryAccess
{
    void AddCategory(Category category);
    Category GetCategoryByID(int categoryId);
    void UpdateCategory(Category category);
    void DeleteCategory(int categoryId);
    List<Category> GetAllCategories();
}
