using Data.Entities;
using Service.Models.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        public TblProduct AddProduct(AddProductModel model);

        public ProductModel GetProductDetail(long productId);

        public TblProduct UpdateProduct(long ProductId, UpdateProductModel model);

        public bool DeleteProduct(long ProductId);
    }
}
