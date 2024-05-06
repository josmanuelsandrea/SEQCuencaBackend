using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class TruckOrderBll
    {
        private readonly TruckOrdersRepository _truckOrderR;
        private readonly CustomerBll _customerB;
        public TruckOrderBll(DbScaniaCuencaContext db)
        {
            _truckOrderR = new TruckOrdersRepository(db);
            _customerB = new CustomerBll(db);
        }
        public WorkOrderResponseModel getWorkOrderById(int id)
        {
            TruckOrder workOrderFound = _truckOrderR.getWorkOrderByNumber(id);
            WorkOrderResponseModel response = new()
            {
                Uid = workOrderFound.Id,
                Fid = workOrderFound.Fid,
                Billquantity = workOrderFound.Billquantity,
                DateField = workOrderFound.DateField,
                Description = workOrderFound.Description,
                Isarchived = workOrderFound.Isarchived,
                Iswarranty = workOrderFound.Iswarranty,
                Kilometers = workOrderFound.Kilometers,
                Labourcost = workOrderFound.Labourcost,
                Storedvolume = workOrderFound.Storedvolume,
                Customer = workOrderFound.CustomerId
            };
            return response;
        }
        public List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<TruckOrder> result = _truckOrderR.getAllWorkOrdersByCustomerId(customerId);
            List<WorkOrderResponseModel> response = new();
            foreach (TruckOrder item in result)
            {
                response.Add(new WorkOrderResponseModel()
                {
                    Uid = item.Id,
                    Fid = item.Fid,
                    Billquantity = item.Billquantity,
                    DateField = item.DateField,
                    Description = item.Description,
                    Isarchived = item.Isarchived,
                    Iswarranty = item.Iswarranty,
                    Kilometers = item.Kilometers,
                    Labourcost = item.Labourcost,
                    Storedvolume = item.Storedvolume,
                    Customer = item.CustomerId,
                });
            }
            return response;
        }
    }
}