using System.Collections.Generic;
using System.Threading.Tasks;
using kontorExpert.Models;

namespace kontorExpert.DataAccess
{
    public interface IProductAccess
    {
        Task<int> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
    }
}
