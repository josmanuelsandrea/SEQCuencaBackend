using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderBll<T> where T : class
    {
        ApiResponse<T?> CreateWorkOrder(WorkOrderRequestModel model);
        ApiResponse<int?> DeleteWorkOrder(int id);
        ApiResponse<T?> EditWorkOrder(WorkOrderEditRequestModel model);
        ApiResponse<List<WorkOrderResponseModel>>  GetAllWorkOrdersByCustomerId(int customerId);
        ApiResponse<List<WorkOrderResponseModel>> GetWorkOrderByVehicleId(int id);
        ApiResponse<WorkOrderResponseModel?> GetWorkOrderById(int id);

        // By type functions
        ApiResponse<List<WorkOrderResponseModel>> GetOrders(string vehicleType);
        ApiResponse<List<WorkOrderResponseModel>> GetWorkOrdersByDateRange(string vehicleType, WorkOrderDate dates);
        ApiResponse<List<WorkOrderResponseModel>> GetWorkOrdersByFid(string vehicleType, WorkOrderRange range);
    }
}