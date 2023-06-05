using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Product;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/productdetails")]
        public IActionResult ProductDetail(long ProductId)
        {
            ProductModel resposne = _productService.GetProductDetail(ProductId);
            if(resposne == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(resposne);
            }
            
        }

        [Authorize(Roles ="Supplier")]
        [HttpPost("/addproduct")]
        public IActionResult AddProduct(AddProductModel model)
        {
            TblProduct Product = _productService.AddProduct(model);
            if(Product != null)
            {
                return Ok("Product Added Successfully!");
            }
            else
            {
                return StatusCode(409,"Product Already Exists");
            }
        }

        [Authorize(Roles = "Supplier")]
        [HttpPut("/updateproduct/{ProductId}")]
        public IActionResult UpdateProduct(long ProductId,UpdateProductModel model)
        {
            if (ProductId != model.ProductId)
            {
                return BadRequest();
            }
            TblProduct Product = _productService.UpdateProduct(ProductId,model);
            if (Product != null)
            {
                return Ok("Product Updated Successfully!");
            }
            else
            {
                return StatusCode(409,"Product doesn't Exists");
            }
        }

        [Authorize(Roles = "Supplier")]
        [HttpDelete("/deleteproduct/{ProductId}")]
        public IActionResult DeleteProduct(long ProductId)
        {
            bool Product = _productService.DeleteProduct(ProductId);
            if(Product == true)
            {
                return Ok("Product Deleted Successfully!");
            }
            else
            {
                return StatusCode(409, "Product doesn't Exists");
            }
        }
    }
}
