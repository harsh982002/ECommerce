using Data.Data;
using Data.Entities;
using Service.Interfaces;
using Service.Models.Company;

namespace Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly EcommerceContext _db;

        public CompanyService(EcommerceContext db)
        {
            _db = db;
        }

        //method to adding the new company
        public TblCompany AddCompany(AddCompanyModel model)
        {
            var company = _db.TblCompanies.Any(x => x.CompanyName.ToLower() == model.CompanyName.ToLower() && x.Email == model.Email); //checks wheather the company name and email already exist or not.
            if (company)
            {
                return null;
                
            }

            var AddedCompany = new TblCompany();
            {
                AddedCompany.CompanyName = model.CompanyName;
                AddedCompany.CompanyAddress = model.CompanyAddress;
                AddedCompany.ContactNumber = model.ContactNumber;
                AddedCompany.Email = model.Email;
                AddedCompany.CountryId = model.CountryId;
            }

            _db.TblCompanies.Add(AddedCompany);
            _db.SaveChanges();

            return AddedCompany;
        }

        //method for deleting the company
        public bool DeleteCompany(long CompanyId)
        {
            var company = _db.TblCompanies.FirstOrDefault(x => x.CompanyId == CompanyId && x.DeletedAt == null); //get the id from database of company which we want to delete and also checks wheather it's already deleted?
            if (company != null)
            {
                company.DeletedAt = DateTime.Now;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //method for get the details of company by CompanyId
        public CompanyModel GetCompanyDetail(long CompanyId)
        {
            var Country = _db.TblCountries.ToList();
            var company = _db.TblCompanies.Where(x => x.CompanyId == CompanyId).FirstOrDefault(); //find the details of company by Id from table
            if (company != null)
            {
                return new CompanyModel
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    ContactNumber = company.ContactNumber,
                    Email = company.Email,
                    Country = company.Country.CountryName,
                };
            }
            else
            {
                return null;
            }
        }

        //method for updating the company details
        public TblCompany UpdateCompany(long CompanyId,UpdateCompanyModel model)
        {
            var companydetails = _db.TblCompanies.Where(x => (x.CompanyName == model.CompanyName && x.Email == model.Email) && x.CompanyId != model.CompanyId).AsQueryable(); //checks wheather the company name and email already exist or not
            if (companydetails.Any())
            {
                return null;
            }

            var company = _db.TblCompanies.Find(CompanyId); //get the id of company which we want to update
            if (company != null && company.DeletedAt == null)
            {
                company.CompanyName = model.CompanyName;
                company.CompanyAddress = model.CompanyAddress;
                company.ContactNumber = model.ContactNumber;
                company.Email = model.Email;
                company.CountryId = model.CountryId;
                company.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
                return company;
            }

            return null;
        }
    }
}
