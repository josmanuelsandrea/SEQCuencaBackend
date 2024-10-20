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
        private readonly VehicleRepository _VehicleRepository;
        private readonly IMapper _mapper;

        public VehicleBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _BusOrderR = new BusOrdersRepository(_db);
            _VehicleRepository = new VehicleRepository(_db, mapper);
        }

        public Vehicle createVehicle(VehicleCreateRequest model, IMapper mapper)
        {
            return _VehicleRepository.createVehicle(model);
        }

        public Vehicle? EditVehicle(Vehicle data)
        {
            return _VehicleRepository.EditVehicle(data);
        }

        public List<Vehicle> GetVehicleByCooperative(int cooperativeId)
        {
            var result = _VehicleRepository.GetAllVehicles().Where(vehicle => vehicle.CooperativeId == cooperativeId).ToList();
            return result;
        }
    }
}
