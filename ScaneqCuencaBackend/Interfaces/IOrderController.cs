using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderController
    {
        ActionResult<ApiResponse<List<WorkOrderResponseModel?>>> GetWorkOrders(string vehicleType);
        ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWarrantyOrders();
        ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrderByVehicleId(int id);
        ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrderByCustomerId(int id);
        ActionResult<ApiResponse<WorkOrderResponseModel>> GetOrderByFid(int id);
        ActionResult<ApiResponse<WorkOrderResponseModel>> Post(WorkOrderRequestModel data);
        ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrdersByRangeNumber(string vehicleType, WorkOrderRange range);
        ActionResult<List<WorkOrderResponseModel>> GetWorkOrdersByRangeDate(string vehicleType, WorkOrderDate range);
        ActionResult<ApiResponse<WorkOrderResponseModel>> Update(WorkOrderEditRequestModel data);
        ActionResult<ApiResponse<int?>> Delete(int id);
    }
}