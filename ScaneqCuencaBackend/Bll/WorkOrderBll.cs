using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class WorkOrderBll
    {
        private readonly WorkOrderRepository _workOrderR;
        private readonly CustomerBll _customerB;
        public WorkOrderBll(DbScaniaCuencaContext db)
        {
            _workOrderR = new WorkOrderRepository(db);
            _customerB = new CustomerBll(db);
        }
        public WorkOrderResponseModel getWorkOrderById(int id)
        {
            WorkOrder workOrderFound = _workOrderR.getWorkOrderByNumber(id);
            WorkOrderResponseModel response = new()
            {
                Uid = workOrderFound.Uid,
                Fid = workOrderFound.Fid,
                Billquantity = workOrderFound.Billquantity,
                DateField = workOrderFound.DateField,
                Description = workOrderFound.Description,
                Isarchived = workOrderFound.Isarchived,
                Iswarranty = workOrderFound.Iswarranty,
                Kilometers = workOrderFound.Kilometers,
                Labourcost = workOrderFound.Labourcost,
                Storedvolume = workOrderFound.Storedvolume,
                Customer = _customerB.getCustomerById(workOrderFound.CustomerId),
            };
            return response;
        }
        public List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<WorkOrder> result = _workOrderR.getAllWorkOrdersByCustomerId(customerId);
            List<WorkOrderResponseModel> response = new();
            foreach (WorkOrder item in result)
            {
                response.Add(new WorkOrderResponseModel()
                {
                    Uid = item.Uid,
                    Fid = item.Fid,
                    Billquantity = item.Billquantity,
                    DateField = item.DateField,
                    Description = item.Description,
                    Isarchived = item.Isarchived,
                    Iswarranty = item.Iswarranty,
                    Kilometers = item.Kilometers,
                    Labourcost = item.Labourcost,
                    Storedvolume = item.Storedvolume,
                    Customer = _customerB.getCustomerById(item.CustomerId),
                });
            }
            return response;
        }
    }
}