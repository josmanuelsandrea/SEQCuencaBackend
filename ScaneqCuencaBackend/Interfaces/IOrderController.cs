using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderController
    {
        ActionResult<GenericResponse<WorkOrderResponseModel>> GetOrderById(int id);
        List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id);
        List<WorkOrderResponseModel> GetWorkOrders([FromQuery] string vehicleType);
        List<WorkOrderResponseModel> GetWorkOrderByVehicleId(int id);
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderDate range);
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderRange range);
        ActionResult<GenericResponse<WorkOrderResponseModel>> Post([FromBody] WorkOrderRequestModel data);
        ActionResult<GenericResponse<WorkOrderResponseModel>> Update([FromBody] WorkOrderEditRequestModel data);
        ActionResult<GenericResponse<WorkOrderResponseModel>> Delete(int id);
    }
}