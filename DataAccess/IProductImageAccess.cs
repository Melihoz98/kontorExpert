using kontorExpert.Models;

namespace kontorExpert.DataAccess
{
    public interface IProductImageAccess
    {
        int AddProductImage(ProductImage productImage);
        ProductImage GetProductImageById(int imageId);
        void UpdateProductImage(ProductImage productImage);
        void DeleteProductImage(int imageId);
        List<ProductImage> GetProductImagesByProductId(int productId);
    }
}
