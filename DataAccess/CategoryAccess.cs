using kontorExpert.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

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

        public void AddCategory(Category category)
        {
            try
            {
                string insertString = "INSERT INTO Categories (CategoryID, CategoryName) VALUES (@CategoryId, @CategoryName)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@CategoryId", category.CategoryID);
                    createCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    con.Open();
                    createCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding category: {ex.Message}");
                throw;
            }
        }

        public Category GetCategoryByID(int categoryId)
        {
            Category foundCategory = null;

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@CategoryID", categoryId);

                    con.Open();

                    SqlDataReader categoryReader = readCommand.ExecuteReader();

                    if (categoryReader.Read())
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

        public void UpdateCategory(Category category)
        {
            try
            {
                string updateString = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    updateCommand.Parameters.AddWithValue("@CategoryID", category.CategoryID);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
                throw;
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                string deleteString = "DELETE FROM Categories WHERE CategoryID = @CategoryID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@CategoryID", categoryId);

                    con.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw;
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> foundCategories = new List<Category>();

            try
            {
                string queryString = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();

                    SqlDataReader categoryReader = readCommand.ExecuteReader();

                    while (categoryReader.Read())
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
