using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderRepository<T> where T : class
    {
        // By type functions
        List<T> GetOrders(string vehicleType);
        List<T> GetBusOrdersByDateRange(string vehicleType, WorkOrderDate dates);
        List<T> GetBusOrdersByFidRange(string vehicleType, WorkOrderRange range);

        // General functions
        List<T> GetAllWorkOrdersByCustomerId(int id);
        List<T> GetOrderByVehicleId(int id);
        T? GetWorkOrderByNumber(int id);
        T CreateWorkOrder(BusOrder model);
        T? DeleteWorkOrder(int id);
        T? EditWorkOrder(WorkOrderEditRequestModel model);
    }
}