using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class TruckOrderBll : IOrderBll<TruckOrder>
    {
        private readonly TruckOrdersRepository _truckOrderR;
        private readonly CustomerBll _customerB;
        private readonly IMapper _mapper;
        public TruckOrderBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _truckOrderR = new TruckOrdersRepository(db);
            _customerB = new CustomerBll(db, mapper);
            _mapper = mapper;
        }

        public List<WorkOrderResponseModel> GetAll()
        {
            var response = _truckOrderR.GetOrders();
            var mappingResult = _mapper.Map<List<WorkOrderResponseModel>>(response);

            return mappingResult;
        }

        public WorkOrderResponseModel GetWorkOrderById(int id)
        {
            TruckOrder? workOrderFound = _truckOrderR.GetWorkOrderByNumber(id);
            var mappingResult = _mapper.Map<WorkOrderResponseModel>(workOrderFound);

            return mappingResult;
        }

        public List<WorkOrderResponseModel> GetAllWorkOrdersByCustomerId(int customerId)
        {
            List<TruckOrder> result = _truckOrderR.GetAllWorkOrdersByCustomerId(customerId);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

            return response;
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByFid(WorkOrderRange range)
        {
            var results = _truckOrderR.GetOrdersByFidRange(range);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(results);

            return response;
        }

        public TruckOrder CreateWorkOrder(WorkOrderRequestModel model)
        {
            var mapModel = _mapper.Map<BusOrder>(model);
            return _truckOrderR.CreateWorkOrder(mapModel);
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByDateRange(WorkOrderDate dates)
        {
            var results = _truckOrderR.GetOrdersByDateRange(dates);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(results);

            return response;
        }

        public TruckOrder? EditWorkOrder(WorkOrderEditRequestModel model)
        {
            return _truckOrderR.EditWorkOrder(model);
        }

        public TruckOrder? DeleteWorkOrder(int id)
        {
            return _truckOrderR.DeleteWorkOrder(id);
        }
    }
}