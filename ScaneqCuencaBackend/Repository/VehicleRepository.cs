using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Repository
{
    public class VehicleRepository
    {
        private readonly SeqcuencabackendContext _db;
        private readonly IMapper _mapper;
        public VehicleRepository(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Vehicle getVehicleById(int id)
        {
            return _db.Vehicles.Where(x => x.Id == id).First();
        }

        public List<Vehicle> getVehiclesByCustomerId(int id)
        {
            return _db.Vehicles.Where(x => x.CustomerId == id).ToList();
        }

        public Vehicle? createVehicle(VehicleCreateRequest model)
        {
            var mapped_data = _mapper.Map<Vehicle>(model);
            try
            {
                _db.Vehicles.Add(mapped_data);
                _db.SaveChanges();
                return mapped_data;
            } catch (Exception)
            {
                return null;
            }
        }
    }
}
