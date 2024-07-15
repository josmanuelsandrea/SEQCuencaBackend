using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderController
    {
        IActionResult Delete(int id);
        WorkOrderResponseModel GetOrderById(int id);
        List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id);
        List<WorkOrderResponseModel> GetWorkOrders([FromQuery] string vehicleType);
        List<WorkOrderResponseModel> GetWorkOrderByVehicleId(int id);
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderDate range);
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderRange range);
        IActionResult Post([FromBody] WorkOrderRequestModel data);
        IActionResult Update([FromBody] WorkOrderEditRequestModel data);
    }
}