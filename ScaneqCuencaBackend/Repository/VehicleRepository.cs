using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public List<Vehicle> GetAllVehicles()
        {
            return _db.Vehicles.Include(v => v.Customer).Include(v => v.Cooperative).ToList();
        }

        public Vehicle getVehicleById(int id)
        {
            return _db.Vehicles.Where(x => x.Id == id).Include(x => x.Cooperative).First();
        }

        public List<Vehicle> getVehiclesByCustomerId(int id)
        {
            return _db.Vehicles.Where(x => x.CustomerId == id).Include(x => x.Cooperative).ToList();
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

        public Vehicle? EditVehicle(Vehicle model)
        {
            var foundVehicle = _db.Vehicles.FirstOrDefault(vehicle => vehicle.Id == model.Id);
            if (foundVehicle == null)
            {
                return null;
            }

            var customerId = foundVehicle.CustomerId;
            _db.Entry(foundVehicle).CurrentValues.SetValues(model);
            foundVehicle.CustomerId = customerId;
            _db.SaveChanges();

            return foundVehicle;
        }
    }
}
