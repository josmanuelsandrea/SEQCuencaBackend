using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class VehicleBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly BusOrdersRepository _BusOrderR;
        private readonly TruckOrdersRepository _TruckOrdersR;
        private readonly VehicleRepository _VehicleRepository;
        private readonly IMapper _mapper;

        public VehicleBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _BusOrderR = new BusOrdersRepository(_db);
            _TruckOrdersR = new TruckOrdersRepository(_db);
            _VehicleRepository = new VehicleRepository(_db, mapper);
        }

        public List<WorkOrderResponseModel> getWorkOrderByVehicleId(int id, string type)
        {
            if (type == "Bus")
            {
                List<BusOrder> result = _BusOrderR.GetWorkOrderByVehicleId(id);
                var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

                return response;
            }
            else
            {
                List<TruckOrder> result = _TruckOrdersR.GetWorkOrderByVehicleId(id);
                var response = _mapper.Map<List<WorkOrderResponseModel>>(result);

                return response;
            }
        }
        public Vehicle createVehicle(VehicleCreateRequest model, IMapper mapper)
        {
            return _VehicleRepository.createVehicle(model);
        }

        public Vehicle? EditVehicle(Vehicle data)
        {
            return _VehicleRepository.EditVehicle(data);
        }
    }
}
