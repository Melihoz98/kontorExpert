using kontorExpert.Models;

namespace kontorExpert.DataAccess
{
    public interface IProductAccess
    {
        int AddProduct(Product product);
        Product GetProductById(int productId);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        List<Product> GetAllProducts();
    }

}
