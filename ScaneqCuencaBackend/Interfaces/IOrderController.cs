using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderController
    {
        IActionResult Delete(int id);
        WorkOrderResponseModel Get(int id);
        List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id);
        List<WorkOrderResponseModel> GetWorkOrders();
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromBody] WorkOrderDate range);
        List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromBody] WorkOrderRange range);
        IActionResult Post([FromBody] WorkOrderRequestModel data);
        IActionResult Put([FromBody] WorkOrderEditRequestModel data);
    }
}