using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderRepository<T> where T : class
    {
        T CreateWorkOrder(BusOrder model);
        T? DeleteWorkOrder(int id);
        T? EditWorkOrder(WorkOrderEditRequestModel model);
        List<T> GetAllWorkOrdersByCustomerId(int id);
        List<T> GetOrders();
        List<T> GetOrdersByDateRange(WorkOrderDate dates);
        List<T> GetOrdersByFidRange(WorkOrderRange range);
        T? GetWorkOrderByNumber(int id);
        List<T> GetWorkOrderByVehicleId(int id);
    }
}