using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class BusOrderBll : IOrderBll<BusOrder>
    {
        private readonly BusOrdersRepository _busOrderR;
        private readonly CustomerBll _customerB;
        private readonly MaintenanceRegistryRepository _maintenanceRegistriesR;
        private readonly SeqcuencabackendContext _db;
        private readonly IMapper _mapper;
        public BusOrderBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _busOrderR = new BusOrdersRepository(db);
            _customerB = new CustomerBll(db, mapper);
            _maintenanceRegistriesR = new MaintenanceRegistryRepository(db);
            _mapper = mapper;
            _db = db;
        }

        public List<WorkOrderResponseModel> GetOrders(string vehicleType)
        {
            var response = _busOrderR.GetOrders(vehicleType);
            var mappingResult = _mapper.Map<List<WorkOrderResponseModel>>(response);

            return mappingResult;
        }

        public WorkOrderResponseModel GetWorkOrderById(int id)
        {
            BusOrder? workOrderFound = _busOrderR.GetWorkOrderByNumber(id);
            var mappingResult = _mapper.Map<WorkOrderResponseModel>(workOrderFound);

            return mappingResult;
        }

        public List<WorkOrderResponseModel> GetWarrantyOrders()
        {
            List<BusOrder> workOrderFound = _busOrderR.GetAllOrders().Where(entity => entity.Iswarranty == true).ToList();
            var response = _mapper.Map<List<WorkOrderResponseModel>>(workOrderFound);

            return response;
        }

        public List<WorkOrderResponseModel> GetWorkOrderByVehicleId(int id)
        {
            List<BusOrder> result = _busOrderR.GetOrderByVehicleId(id);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

            return response;
        }

        public List<WorkOrderResponseModel> GetAllWorkOrdersByCustomerId(int customerId)
        {
            List<BusOrder> result = _busOrderR.GetAllWorkOrdersByCustomerId(customerId);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

            return response;
        }

        public BusOrder? CreateWorkOrder(WorkOrderRequestModel model)
        {
            var mapOrder = _mapper.Map<BusOrder>(model);
            mapOrder.VehicleType = mapOrder.VehicleType!.ToLower();

            var mapRegistries = _mapper.Map<List<MaintenanceRegistry>>(model.maintenances);

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var OperationResultWorkOrder = _busOrderR.CreateWorkOrder(mapOrder);
                    if (OperationResultWorkOrder == null)
                    {
                        transaction.Rollback();
                        return null;
                    }

                    mapRegistries.ForEach(registry => registry.OrderFkId = OperationResultWorkOrder.Id);

                    var OperationResultMaintenanceRegistry = _maintenanceRegistriesR.AddMultiple(mapRegistries);
                    if (OperationResultMaintenanceRegistry == null)
                    {
                        // Si falla la adición de registros de mantenimiento, revierte la transacción
                        transaction.Rollback();
                        return null;
                    }

                    // Confirma la transacción
                    transaction.Commit();

                    // Retorna la orden de trabajo creada
                    return OperationResultWorkOrder;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByFid(string vehicleType, WorkOrderRange range)
        {
            var results = _busOrderR.GetBusOrdersByFidRange(vehicleType, range);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(results);

            return response;
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByDateRange(string vehicleType, WorkOrderDate dates)
        {
            var results = _busOrderR.GetBusOrdersByDateRange(vehicleType, dates);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(results);

            return response;
        }

        public BusOrder? EditWorkOrder(WorkOrderEditRequestModel model)
        {
            var maintenanceRegistryMapping = _mapper.Map<List<MaintenanceRegistry>>(model.maintenances);
            var foundWorkOrder = _busOrderR.GetWorkOrderByNumber(model.Fid);

            if (foundWorkOrder == null)
            {
                return null;
            }

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var OperationResultOrder = _busOrderR.EditWorkOrder(model);
                    if (OperationResultOrder == null)
                    {
                        transaction.Rollback();
                        return null;
                    }

                    var OperationResultMaintenanceRegistries = _maintenanceRegistriesR.UpdateOrderRegistries(foundWorkOrder.Id, maintenanceRegistryMapping);
                    if (OperationResultMaintenanceRegistries == null)
                    {
                        transaction.Rollback();
                        return null;
                    }

                    transaction.Commit();
                    return foundWorkOrder;
                } catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public BusOrder? DeleteWorkOrder(int id)
        {
            return _busOrderR.DeleteWorkOrder(id);
        }
    }
}
