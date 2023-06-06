using Common.AppSettings;
using Data.Data;
using Data.Entities;
using Service.Interfaces;
using Service.Models.Supplier;

namespace Service.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly EcommerceContext _db;

        public SupplierService(EcommerceContext db)
        {
            _db = db;
        }
        public long? AddSupplier(AddSupplierModel model)
        {
            var supplier = _db.TblSuppliers.Any(x => x.SupplierName.ToLower() == model.SupplierName.ToLower() && x.Email == model.Email); //checks wheather the company name and email already exist or not.
            if (supplier)
            {
                return null;

            }

            var AddedSupplier = new TblSupplier();
            {
                AddedSupplier.SupplierName = model.SupplierName;
                AddedSupplier.ContactNumber = model.ContactNumber;
                AddedSupplier.Email = model.Email;
                AddedSupplier.CompanyId = model.CompanyId;
            }

            _db.TblSuppliers.Add(AddedSupplier);
            _db.SaveChanges();

            return AddedSupplier.SupplierId;
        }

        public bool DeleteSupplier(short SupplierId)
        {
            var supplier = _db.TblSuppliers.FirstOrDefault(x => x.SupplierId == SupplierId && x.DeletedAt == null); //get the id from database of company which we want to delete and also checks wheather it's already deleted?
            if (supplier != null)
            {
                supplier.DeletedAt = DateTime.Now;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public SupplierModel GetSupplierDetailsById(short SupplierId)
        {
            var company = _db.TblCompanies.ToList();
            var supplier = _db.TblSuppliers.Where(x => x.SupplierId == SupplierId && x.Status == true && x.DeletedAt == null).FirstOrDefault(); //find the details of company by Id from table
            if (supplier != null)
            {
                return new SupplierModel
                {
                    SupplierName = supplier.SupplierName,
                    ContactNumber = supplier.ContactNumber,
                    Email = supplier.Email,
                    Company = supplier.Company.CompanyName,
                };
            }
            else
            {
                return null;
            }
        }

        public List<SupplierModel> GetSupplierDetails()
        {
            List<SupplierModel> SupplierList = (from s in _db.TblSuppliers
                                                where (s.Status == true && s.DeletedAt == null)
                                                select new SupplierModel
                                                {
                                                    SupplierName = s.SupplierName,
                                                    ContactNumber = s.ContactNumber,
                                                    Email = s.Email,
                                                    Company = s.Company.CompanyName,

                                                }).ToList();
            return SupplierList;
        }

        public bool SupplierStatus(short SupplierId, bool status)
        {
            var supplier = _db.TblSuppliers.Find(SupplierId);
            if (supplier != null && supplier.DeletedAt == null)
            {
                supplier.Status = status;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ResponseModel UpdateSupplier(short SupplierId, UpdateSupplierModel model)
        {
            var supplierdetails = _db.TblSuppliers.Where(x => (x.SupplierName == model.SupplierName && x.Email == model.Email) && x.SupplierId != model.SupplierId).AsQueryable(); //checks wheather the company name and email already exist or not
            if (supplierdetails.Any())
            {
                return null;
            }

            var supplier = _db.TblSuppliers.Find(SupplierId); //get the id of company which we want to update
            if (supplier != null && supplier.DeletedAt == null)
            {
                supplier.SupplierName = model?.SupplierName;
                supplier.ContactNumber = model?.ContactNumber;
                supplier.Email = model?.Email;
                supplier.CompanyId = model.CompanyId;
                _db.SaveChanges();
                return new ResponseModel()
                {
                    Id = SupplierId,
                    Message = "Supplier Updated Successfully.",
                    StatusCode = 200,
                };
            }

            return new ResponseModel()
            {
                Id = SupplierId,
                Message = "Supplier Doesn't Exist!",
                StatusCode = 401,
            };
        }
    }
}
