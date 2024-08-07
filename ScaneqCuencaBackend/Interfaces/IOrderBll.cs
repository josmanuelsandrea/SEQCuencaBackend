﻿using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Interfaces
{
    public interface IOrderBll<T> where T : class
    {
        T CreateWorkOrder(WorkOrderRequestModel model);
        int? DeleteWorkOrder(int id);
        T? EditWorkOrder(WorkOrderEditRequestModel model);
        List<WorkOrderResponseModel> GetAllWorkOrdersByCustomerId(int customerId);
        List<WorkOrderResponseModel> GetWorkOrderByVehicleId(int id);
        WorkOrderResponseModel GetWorkOrderById(int id);

        // By type functions
        List<WorkOrderResponseModel> GetOrders(string vehicleType);
        List<WorkOrderResponseModel> GetWorkOrdersByDateRange(string vehicleType, WorkOrderDate dates);
        List<WorkOrderResponseModel> GetWorkOrdersByFid(string vehicleType, WorkOrderRange range);
    }
}