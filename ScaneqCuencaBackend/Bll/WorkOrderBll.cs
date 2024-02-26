using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Helpers;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public static class WorkOrderBll
    {
        public static WorkOrderResponseModel getWorkOrderById(int id)
        {
            WorkOrder workOrderFound = WorkOrderRepository.getWorkOrderByNumber(id);
            WorkOrderResponseModel response = WorkOrderMapper.ModelMapper(workOrderFound);
            return response;
        }
        public static List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<WorkOrder> result = WorkOrderRepository.getAllWorkOrdersByCustomerId(customerId);
            List<WorkOrderResponseModel> response = WorkOrderMapper.ListModelMapper(result);
            return response;
        }
    }
}