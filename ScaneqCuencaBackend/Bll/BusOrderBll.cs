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

        public WorkOrderResponseModel getWorkOrderById(int id)
        {
            BusOrder workOrderFound = _busOrderR.getWorkOrderByNumber(id);
            WorkOrderResponseModel response = new()
            {
                Uid = workOrderFound.Id,
                Fid = workOrderFound.Fid,
                DateField = workOrderFound.DateField,
                Description = workOrderFound.Description,
                Isarchived = workOrderFound.Isarchived,
                Iswarranty = workOrderFound.Iswarranty,
                Kilometers = workOrderFound.Kilometers,
                Storedvolume = workOrderFound.Storedvolume,
                Customer = workOrderFound.CustomerId
            };

            return response;
        }

        public List<WorkOrderResponseModel> getAllWorkOrdersByCustomerId(int customerId)
        {
            List<BusOrder> result = _busOrderR.getAllWorkOrdersByCustomerId(customerId);
            List<WorkOrderResponseModel> response = new();
            foreach (BusOrder item in result)
            {
                response.Add(new WorkOrderResponseModel()
                {
                    Uid = item.Id,
                    Fid = item.Fid,
                    DateField = item.DateField,
                    Description = item.Description,
                    Isarchived = item.Isarchived,
                    Iswarranty = item.Iswarranty,
                    Kilometers = item.Kilometers,
                    Storedvolume = item.Storedvolume,
                    Customer = item.CustomerId,
                });
            }

            return response;
        }

        public BusOrder createWorkOrder(WorkOrderRequestModel model)
        {
            var mapModel = _mapper.Map<BusOrder>(model);
            return _busOrderR.createWorkOrder(mapModel);
        }
    }
}
