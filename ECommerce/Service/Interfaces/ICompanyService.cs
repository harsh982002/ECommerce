using Data.Entities;
using Service.Models.Company;

namespace Service.Interfaces
{
    public interface ICompanyService
    {
        public CompanyModel GetCompanyDetail(long CompanyId);

        public TblCompany AddCompany(AddCompanyModel model);

        public TblCompany UpdateCompany(long CompanyId,UpdateCompanyModel model);

        public bool DeleteCompany(long CompanyId);
    }
}
