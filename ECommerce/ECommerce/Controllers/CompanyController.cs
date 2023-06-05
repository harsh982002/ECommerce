using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Company;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet("/companydetails")]
        public IActionResult CompanyDetails(long CompanyId)
        {
            CompanyModel response = _companyService.GetCompanyDetail(CompanyId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/addcompany")]
        public IActionResult AddCompany(AddCompanyModel model)
        {
            TblCompany Company = _companyService.AddCompany(model);
            if (Company != null)
            {
                return Ok("Company Added successfully");
            }
            else
            {
                return StatusCode(409, "Company Already Exist");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("/updatecompany/{CompanyId}")]
        public IActionResult UpdateCompany(long CompanyId, UpdateCompanyModel model)
        {
            if (CompanyId != model.CompanyId)
            {
                return BadRequest();
            }
            TblCompany company = _companyService.UpdateCompany(CompanyId, model);
            if (company != null)
            {
                return Ok("Company Updated successfully");
            }
            else
            {
                return StatusCode(409, "Company doesn't Exists!");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("/deletecompany/{CompanyId}")]
        public IActionResult DeleteCompany(long CompanyId)
        {
            bool Company = _companyService.DeleteCompany(CompanyId);
            if (Company == true)
            {
                return Ok("Company Removed Successfully");
            }
            else
            {
                return StatusCode(409, "Company doesn't Exists!");
            }
        }
    }
}
