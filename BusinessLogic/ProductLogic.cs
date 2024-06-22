using kontorExpert.DataAccess;
using kontorExpert.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kontorExpert.BusinessLogic
{
    public class ProductLogic
    {
        private readonly IProductAccess _productAccess;

        public ProductLogic(IProductAccess productAccess)
        {
            _productAccess = productAccess;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            // Perform any necessary business logic validation here
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            // Call the data access layer to add the product
            return await _productAccess.AddProductAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            // Perform any necessary business logic validation here
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID.");
            }

            // Call the data access layer to get the product by ID
            return await _productAccess.GetProductByIdAsync(productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Perform any necessary business logic validation here
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (product.ProductID <= 0)
            {
                throw new ArgumentException("Invalid product ID.");
            }

            // Call the data access layer to update the product
            await _productAccess.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            // Perform any necessary business logic validation here
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID.");
            }

            // Call the data access layer to delete the product
            await _productAccess.DeleteProductAsync(productId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            // Call the data access layer to get all products
            return await _productAccess.GetAllProductsAsync();
        }
    }
}
