using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Supplier;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("/supplierdetails")]
        public IActionResult SupplierDetails(short SupplierId)
        {
            SupplierModel response = _supplierService.GetSupplierDetails(SupplierId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpPost("/addsupplier")]
        public IActionResult AddSupplier(AddSupplierModel model)
        {
            TblSupplier supplier = _supplierService.AddSupplier(model);
            if (supplier != null)
            {
                return Ok("Supplier Added successfully");
            }
            else
            {
                return StatusCode(409, "Supplier Already Exist");
            }
        }

        [Authorize(Roles = "Supplier")]
        [HttpPut("/updatesupplier/{SupplierId}")]
        public IActionResult UpdateSupplier(short SupplierId, UpdateSupplierModel model)
        {
            if (SupplierId != model.SupplierId)
            {
                return BadRequest();
            }
            TblSupplier supplier = _supplierService.UpdateSupplier(SupplierId, model);
            if (supplier != null)
            {
                return Ok("Supplier Updated successfully");
            }
            else
            {
                return StatusCode(409, "Supplier doesn't Exists!");
            }
        }

        [Authorize(Roles = "Supplier,Admin")]
        [HttpDelete("/deletesupplier/{CompanyId}")]
        public IActionResult DeleteSupplier(short SupplierId)
        {
            bool supplier = _supplierService.DeleteSupplier(SupplierId);
            if (supplier == true)
            {
                return Ok("Supplier Removed Successfully");
            }
            else
            {
                return StatusCode(409, "Supplier doesn't Exists!");
            }
        }
    }
}
