using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class BusOrderBll
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

        public List<WorkOrderResponseModel> GetAll()
        {
            var response = _busOrderR.GetOrders();
            var mappingResult = _mapper.Map<List<WorkOrderResponseModel>>(response);

            return mappingResult;
        }

        public WorkOrderResponseModel getWorkOrderById(int id)
        {
            BusOrder workOrderFound = _busOrderR.getWorkOrderByNumber(id);
            var mappingResult = _mapper.Map<WorkOrderResponseModel>(workOrderFound);

            return mappingResult;
        }

        public List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<BusOrder> result = _busOrderR.getAllWorkOrdersByCustomerId(customerId);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

            return response;
        }

        public BusOrder createWorkOrder(WorkOrderRequestModel model)
        {
            var mapModel = _mapper.Map<BusOrder>(model);
            return _busOrderR.createWorkOrder(mapModel);
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByFid(WorkOrderRange range)
        {
            var results = _busOrderR.GetOrdersByFidRange(range);
            var response = _mapper.Map<List<WorkOrderResponseModel>>(results);

            return response;
        }

        public List<WorkOrderResponseModel> GetWorkOrdersByDateRange(WorkOrderDate dates)
        {
            var results = _busOrderR.GetOrdersByDateRange(dates);
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
