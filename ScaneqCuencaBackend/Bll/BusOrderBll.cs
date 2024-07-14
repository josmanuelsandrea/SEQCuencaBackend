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
        private readonly IMapper _mapper;
        public BusOrderBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _busOrderR = new BusOrdersRepository(db);
            _customerB = new CustomerBll(db, mapper);
            _mapper = mapper;
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

        public BusOrder CreateWorkOrder(WorkOrderRequestModel model)
        {
            var mapModel = _mapper.Map<BusOrder>(model);
            mapModel.VehicleType = mapModel.VehicleType!.ToLower();
            return _busOrderR.CreateWorkOrder(mapModel);
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
            return _busOrderR.EditWorkOrder(model);
        }

        public BusOrder? DeleteWorkOrder(int id)
        {
            return _busOrderR.DeleteWorkOrder(id);
        }
    }
}
