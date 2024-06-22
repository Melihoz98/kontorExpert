using kontorExpert.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace kontorExpert.DataAccess
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly string _connectionString;

        public CategoryAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                string insertString = "INSERT INTO Categories (CategoryID, CategoryName) VALUES (@CategoryId, @CategoryName)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@CategoryId", category.CategoryID);
                    createCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    await con.OpenAsync();
                    await createCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding category: {ex.Message}");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIDAsync(int categoryId)
        {
            Category foundCategory = null;

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@CategoryID", categoryId);

                    await con.OpenAsync();
                    SqlDataReader categoryReader = await readCommand.ExecuteReaderAsync();

                    if (await categoryReader.ReadAsync())
                    {
                        foundCategory = GetCategoryFromReader(categoryReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving category by ID: {ex.Message}");
                throw;
            }

            return foundCategory;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                string updateString = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    updateCommand.Parameters.AddWithValue("@CategoryID", category.CategoryID);

                    await con.OpenAsync();
                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            try
            {
                string deleteString = "DELETE FROM Categories WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@CategoryID", categoryId);

                    await con.OpenAsync();
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            List<Category> foundCategories = new List<Category>();

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();
                    SqlDataReader categoryReader = await readCommand.ExecuteReaderAsync();

                    while (await categoryReader.ReadAsync())
                    {
                        Category category = GetCategoryFromReader(categoryReader);
                        foundCategories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all categories: {ex.Message}");
                throw;
            }

            return foundCategories;
        }

        private Category GetCategoryFromReader(SqlDataReader categoryReader)
        {
            int categoryId = categoryReader.GetInt32(categoryReader.GetOrdinal("CategoryID"));
            string categoryName = categoryReader.GetString(categoryReader.GetOrdinal("CategoryName"));

            return new Category { CategoryID = categoryId, CategoryName = categoryName };
        }
    }
}
