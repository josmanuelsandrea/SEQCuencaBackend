using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class VehicleBll
    {
        private readonly VehicleRepository _VehicleRepository;

        public VehicleBll(SeqcuencabackendContext db, IMapper mapper, BusOrdersRepository busOrderR, VehicleRepository vehicleRepository)
        {
            _VehicleRepository = vehicleRepository;
        }

        public Vehicle createVehicle(VehicleCreateRequest model)
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
