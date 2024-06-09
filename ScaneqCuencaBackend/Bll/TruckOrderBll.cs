using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class TruckOrderBll
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
        public WorkOrderResponseModel getWorkOrderById(int id)
        {
            TruckOrder workOrderFound = _truckOrderR.getWorkOrderByNumber(id);
            var mappingResult = _mapper.Map<WorkOrderResponseModel>(workOrderFound);
            return mappingResult;
        }
        public List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<TruckOrder> result = _truckOrderR.getAllWorkOrdersByCustomerId(customerId);
            var mappingResult = _mapper.Map<List<WorkOrderResponseModel>>(result);
            
            return mappingResult;
        }
    }
}