using Data.Data;
using Data.Entities;
using Service.Interfaces;
using Service.Models.Product;

namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly EcommerceContext _db;

        public ProductService(EcommerceContext db)
        {
            _db = db;
        }

        //Method for adding the Product
        public TblProduct AddProduct(AddProductModel model)
        {
            var Product = _db.TblProducts.Any(x => x.ProductName.ToLower() == model.ProductName.ToLower() && x.SupplierId == model.SupplierId); //checks wheather the productname and supplier already exist or not.
            if (Product)
            {
                return null;

            }

            var AddedProduct = new TblProduct();
            {
                AddedProduct.ProductName = model.ProductName;
                AddedProduct.Description = model.Description;
                AddedProduct.Price = model.Price;
                AddedProduct.StokeKeepingUnit = model.StokeKeepingUnit;
                AddedProduct.CategoryId = model.CategoryId;
                AddedProduct.CompanyId = model.CompanyId;
                AddedProduct.SubCategoryid = model.SubCategoryid;
                AddedProduct.SupplierId = model.SupplierId;
                AddedProduct.AvailableStock = model.AvailableStock;

            }

            _db.TblProducts.Add(AddedProduct);
            _db.SaveChanges();

            return AddedProduct;
        }

        //Method for deleting the Product
        public bool DeleteProduct(long ProductId)
        {
            var Product = _db.TblProducts.FirstOrDefault(x => x.ProductId == ProductId && x.DeletedAt == null); //get the id from database of Product which we want to delete and also checks wheather it's already deleted?
            if (Product != null)
            {
                Product.DeletedAt = DateTime.Now;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Method for get detail of the Product
        public ProductModel GetProductDetail(long productId)
        {
            var Category = _db.TblCategories.ToList();
            var Subcategory = _db.TblSubCategories.ToList();
            var Company = _db.TblCompanies.ToList();
            var Supplier = _db.TblSuppliers.ToList();
            var product = _db.TblProducts.Where(x => x.ProductId == productId).FirstOrDefault();  //find the details of product by Id from table
            if (product != null)
            {
                return new ProductModel
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Discount = product.Discount,
                    Price = product.Price,
                    Category = product.Category.CategoryName,
                    SubCategory = product.SubCategory.SubCategoryName,
                    Company = product.Company.CompanyName,
                    Supplier = product.Supplier.SupplierName,
                    AvailableStock = product.AvailableStock,
                };
            }
            else
            {
                return null;
            }
        }

        //Method for updating the Product detail
        public TblProduct UpdateProduct(long ProductId, UpdateProductModel model)
        {
            var productdetails = _db.TblProducts.Where(x => (x.ProductName == model.ProductName && x.SupplierId == model.SupplierId) && x.ProductId != model.ProductId).AsQueryable(); //checks wheather the product name and supplier already exist or not
            if (productdetails.Any())
            {
                return null;
            }

            var Product = _db.TblProducts.Find(ProductId); //get the id of product which we want to update
            if (Product != null && Product.DeletedAt == null)
            {
                Product.ProductName = model.ProductName;
                Product.Description = model.Description;
                Product.Price = model.Price;
                Product.StokeKeepingUnit = model.StokeKeepingUnit;
                Product.CategoryId = model.CategoryId;
                Product.CompanyId = model.CompanyId;
                Product.SubCategoryid = model.SubCategoryid;
                Product.SupplierId = model.SupplierId;
                Product.AvailableStock = model.AvailableStock;
                Product.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
                return Product;
            }

            return null;
        }
    }
}
