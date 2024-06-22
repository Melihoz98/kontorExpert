using Microsoft.Extensions.Configuration;
using kontorExpert.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace kontorExpert.DataAccess
{
    public class ProductImageAccess : IProductImageAccess
    {
        private readonly string _connectionString;

        public ProductImageAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public async Task<int> AddProductImageAsync(ProductImage productImage)
        {
            try
            {
                string insertString = "INSERT INTO ProductImages (ProductID, ImageUrl) " +
                                      "VALUES (@ProductID, @ImageUrl); " +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@ProductID", productImage.ProductID);
                    createCommand.Parameters.AddWithValue("@ImageUrl", productImage.ImageUrl);

                    await con.OpenAsync();
                    int imageId = Convert.ToInt32(await createCommand.ExecuteScalarAsync());
                    return imageId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product image: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int imageId)
        {
            ProductImage foundImage = null;

            try
            {
                string queryString = "SELECT ImageID, ProductID, ImageUrl FROM ProductImages WHERE ImageID = @ImageID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@ImageID", imageId);

                    await con.OpenAsync();

                    SqlDataReader imageReader = await readCommand.ExecuteReaderAsync();

                    if (await imageReader.ReadAsync())
                    {
                        foundImage = GetProductImageFromReader(imageReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product image by ID: {ex.Message}");
                throw;
            }

            return foundImage;
        }

        public async Task UpdateProductImageAsync(ProductImage productImage)
        {
            try
            {
                string updateString = "UPDATE ProductImages SET ProductID = @ProductID, ImageUrl = @ImageUrl WHERE ImageID = @ImageID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@ProductID", productImage.ProductID);
                    updateCommand.Parameters.AddWithValue("@ImageUrl", productImage.ImageUrl);
                    updateCommand.Parameters.AddWithValue("@ImageID", productImage.ImageID);

                    await con.OpenAsync();
                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product image: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductImageAsync(int imageId)
        {
            try
            {
                string deleteString = "DELETE FROM ProductImages WHERE ImageID = @ImageID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
                {
                    deleteCommand.Parameters.AddWithValue("@ImageID", imageId);

                    await con.OpenAsync();
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product image: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductImage>> GetProductImagesByProductIdAsync(int productId)
        {
            List<ProductImage> foundImages = new List<ProductImage>();

            try
            {
                string queryString = "SELECT ImageID, ProductID, ImageUrl FROM ProductImages WHERE ProductID = @ProductID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@ProductID", productId);

                    await con.OpenAsync();

                    SqlDataReader imageReader = await readCommand.ExecuteReaderAsync();

                    while (await imageReader.ReadAsync())
                    {
                        ProductImage image = GetProductImageFromReader(imageReader);
                        foundImages.Add(image);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product images by product ID: {ex.Message}");
                throw;
            }

            return foundImages;
        }

        private ProductImage GetProductImageFromReader(SqlDataReader imageReader)
        {
            int imageID = imageReader.GetInt32(imageReader.GetOrdinal("ImageID"));
            int productID = imageReader.GetInt32(imageReader.GetOrdinal("ProductID"));
            string imageUrl = imageReader.GetString(imageReader.GetOrdinal("ImageUrl"));

            return new ProductImage
            {
                ImageID = imageID,
                ProductID = productID,
                ImageUrl = imageUrl
            };
        }
    }
}