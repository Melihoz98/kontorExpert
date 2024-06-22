using kontorExpert.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kontorExpert.DataAccess
{
    public interface IProductImageAccess
    {
        Task<int> AddProductImageAsync(ProductImage productImage);
        Task<ProductImage> GetProductImageByIdAsync(int imageId);
        Task UpdateProductImageAsync(ProductImage productImage);
        Task DeleteProductImageAsync(int imageId);
        Task<List<ProductImage>> GetProductImagesByProductIdAsync(int productId);
    }
}