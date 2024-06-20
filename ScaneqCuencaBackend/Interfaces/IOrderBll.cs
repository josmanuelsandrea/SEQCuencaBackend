using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderBll<T> where T : class
    {
        T CreateWorkOrder(WorkOrderRequestModel model);
        T? DeleteWorkOrder(int id);
        T? EditWorkOrder(WorkOrderEditRequestModel model);
        List<WorkOrderResponseModel> GetAll();
        List<WorkOrderResponseModel> GetAllWorkOrdersByCustomerId(int customerId);
        WorkOrderResponseModel GetWorkOrderById(int id);
        List<WorkOrderResponseModel> GetWorkOrdersByDateRange(WorkOrderDate dates);
        List<WorkOrderResponseModel> GetWorkOrdersByFid(WorkOrderRange range);
    }
}