using kontorExpert.Models;
using kontorExpert.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kontorExpert.BusinessLogic
{
    public class ProductImageLogic
    {
        private readonly IProductImageAccess _productImageAccess;

        public ProductImageLogic(IProductImageAccess productImageAccess)
        {
            _productImageAccess = productImageAccess ?? throw new ArgumentNullException(nameof(productImageAccess));
        }

        public async Task<int> AddProductImageAsync(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage));
            }

            if (productImage.ProductID <= 0)
            {
                throw new ArgumentException("Invalid ProductID.");
            }

            if (string.IsNullOrEmpty(productImage.ImageUrl))
            {
                throw new ArgumentException("ImageUrl cannot be null or empty.");
            }

            return await _productImageAccess.AddProductImageAsync(productImage);
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int imageId)
        {
            if (imageId <= 0)
            {
                throw new ArgumentException("Invalid ImageID.");
            }

            return await _productImageAccess.GetProductImageByIdAsync(imageId);
        }

        public async Task UpdateProductImageAsync(ProductImage productImage)
        {
            if (productImage == null)
            {
                throw new ArgumentNullException(nameof(productImage));
            }

            if (productImage.ImageID <= 0)
            {
                throw new ArgumentException("Invalid ImageID.");
            }

            if (productImage.ProductID <= 0)
            {
                throw new ArgumentException("Invalid ProductID.");
            }

            if (string.IsNullOrEmpty(productImage.ImageUrl))
            {
                throw new ArgumentException("ImageUrl cannot be null or empty.");
            }

            await _productImageAccess.UpdateProductImageAsync(productImage);
        }

        public async Task DeleteProductImageAsync(int imageId)
        {
            if (imageId <= 0)
            {
                throw new ArgumentException("Invalid ImageID.");
            }

            await _productImageAccess.DeleteProductImageAsync(imageId);
        }

        public async Task<List<ProductImage>> GetProductImagesByProductIdAsync(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid ProductID.");
            }

            return await _productImageAccess.GetProductImagesByProductIdAsync(productId);
        }
    }
}
