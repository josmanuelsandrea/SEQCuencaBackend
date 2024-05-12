using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class VehicleRepository
    {
        private readonly SeqcuencabackendContext _db;
        public VehicleRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }

        public Vehicle getVehicleById(int id)
        {
            return _db.Vehicles.Where(x => x.Id == id).First();
        }

        public List<Vehicle> getVehiclesByCustomerId(int id)
        {
            return _db.Vehicles.Where(x => x.CustomerId == id).ToList();
        }
    }
}
