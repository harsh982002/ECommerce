using Data.Entities;
using Service.Models.Supplier;

namespace Service.Interfaces
{
    public interface ISupplierService
    {
        public SupplierModel GetSupplierDetails(short SupplierId);

        public TblSupplier AddSupplier(AddSupplierModel model);

        public TblSupplier UpdateSupplier(short SupplierId,UpdateSupplierModel model);

        public bool DeleteSupplier(short SupplierId);
    }
}
