using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using kontorExpert.Models;

public class ParentCategoryAccess : IParentCategoryAccess
{
    private readonly string _connectionString;

    public ParentCategoryAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddParentCategoryAsync(ParentCategory parentCategory)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO ParentCategories (ParentCategoryName) VALUES (@ParentCategoryName)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ParentCategoryName", parentCategory.ParentCategoryName);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<ParentCategory> GetParentCategoryByIDAsync(int parentCategoryId)
    {
        ParentCategory parentCategory = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ParentCategoryID, ParentCategoryName FROM ParentCategories WHERE ParentCategoryID = @ParentCategoryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ParentCategoryID", parentCategoryId);

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                parentCategory = new ParentCategory
                {
                    ParentCategoryID = (int)reader["ParentCategoryID"],
                    ParentCategoryName = (string)reader["ParentCategoryName"]
                };
            }
        }

        return parentCategory;
    }

    public async Task UpdateParentCategoryAsync(ParentCategory parentCategory)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE ParentCategories SET ParentCategoryName = @ParentCategoryName WHERE ParentCategoryID = @ParentCategoryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ParentCategoryName", parentCategory.ParentCategoryName);
            command.Parameters.AddWithValue("@ParentCategoryID", parentCategory.ParentCategoryID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteParentCategoryAsync(int parentCategoryId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM ParentCategories WHERE ParentCategoryID = @ParentCategoryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ParentCategoryID", parentCategoryId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<ParentCategory>> GetAllParentCategoriesAsync()
    {
        List<ParentCategory> parentCategories = new List<ParentCategory>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ParentCategoryID, ParentCategoryName FROM ParentCategories";
            SqlCommand command = new SqlCommand(query, connection);

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ParentCategory parentCategory = new ParentCategory
                {
                    ParentCategoryID = (int)reader["ParentCategoryID"],
                    ParentCategoryName = (string)reader["ParentCategoryName"]
                };

                parentCategories.Add(parentCategory);
            }
        }

        return parentCategories;
    }
}
