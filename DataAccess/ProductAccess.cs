using kontorExpert.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace kontorExpert.DataAccess
{
    public class ProductAccess : IProductAccess
    {
        private readonly string _connectionString;

        public ProductAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            try
            {
                string insertString = "INSERT INTO Products (Name, Description, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed) " +
                                      "VALUES (@Name, @Description, @Price, @StockQuantity, @Color, @Dimensions, @CategoryID, @IsUsed); " +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@Name", product.Name);
                    createCommand.Parameters.AddWithValue("@Description", product.Description);
                    createCommand.Parameters.AddWithValue("@Price", product.Price);
                    createCommand.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    createCommand.Parameters.AddWithValue("@Color", product.Color);
                    createCommand.Parameters.AddWithValue("@Dimensions", product.Dimensions);
                    createCommand.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    createCommand.Parameters.AddWithValue("@IsUsed", product.IsUsed);

                    await con.OpenAsync();
                    int productId = Convert.ToInt32(await createCommand.ExecuteScalarAsync());
                    return productId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            Product foundProduct = null;

            try
            {
                string queryString = "SELECT ProductID, Name, Description, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products WHERE ProductID = @ProductID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@ProductID", productId);

                    await con.OpenAsync();
                    SqlDataReader productReader = await readCommand.ExecuteReaderAsync();

                    if (await productReader.ReadAsync())
                    {
                        foundProduct = GetProductFromReader(productReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product by ID: {ex.Message}");
                throw;
            }

            return foundProduct;
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                string updateString = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, " +
                                      "StockQuantity = @StockQuantity, Color = @Color, Dimensions = @Dimensions, " +
                                      "CategoryID = @CategoryID, IsUsed = @IsUsed WHERE ProductID = @ProductID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@Name", product.Name);
                    updateCommand.Parameters.AddWithValue("@Description", product.Description);
                    updateCommand.Parameters.AddWithValue("@Price", product.Price);
                    updateCommand.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    updateCommand.Parameters.AddWithValue("@Color", product.Color);
                    updateCommand.Parameters.AddWithValue("@Dimensions", product.Dimensions);
                    updateCommand.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    updateCommand.Parameters.AddWithValue("@IsUsed", product.IsUsed);
                    updateCommand.Parameters.AddWithValue("@ProductID", product.ProductID);

                    await con.OpenAsync();
                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                string deleteString = "DELETE FROM Products WHERE ProductID = @ProductID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@ProductID", productId);

                    await con.OpenAsync();
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> foundProducts = new List<Product>();

            try
            {
                string queryString = "SELECT ProductID, Name, Description, Price, StockQuantity, Color, Dimensions, CategoryID, IsUsed FROM Products";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    await con.OpenAsync();
                    SqlDataReader productReader = await readCommand.ExecuteReaderAsync();

                    while (await productReader.ReadAsync())
                    {
                        Product product = GetProductFromReader(productReader);
                        foundProducts.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all products: {ex.Message}");
                throw;
            }

            return foundProducts;
        }

        private Product GetProductFromReader(SqlDataReader productReader)
        {
            int productID = productReader.GetInt32(productReader.GetOrdinal("ProductID"));
            string name = productReader.GetString(productReader.GetOrdinal("Name"));
            string description = productReader.GetString(productReader.GetOrdinal("Description"));
            decimal price = productReader.GetDecimal(productReader.GetOrdinal("Price"));
            int stockQuantity = productReader.GetInt32(productReader.GetOrdinal("StockQuantity"));
            string color = productReader.GetString(productReader.GetOrdinal("Color"));
            string dimensions = productReader.GetString(productReader.GetOrdinal("Dimensions"));
            int categoryID = productReader.GetInt32(productReader.GetOrdinal("CategoryID"));
            bool isUsed = productReader.GetBoolean(productReader.GetOrdinal("IsUsed"));

            return new Product
            {
                ProductID = productID,
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                Color = color,
                Dimensions = dimensions,
                CategoryID = categoryID,
                IsUsed = isUsed
            };
        }
    }
}
