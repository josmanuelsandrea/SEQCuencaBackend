using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class MaintenanceRegistryBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly MaintenanceRegistryRepository _repository;
        public MaintenanceRegistryBll(SeqcuencabackendContext db)
        {
            _db = db;
            _repository = new(db);
        }

        public List<MaintenanceRegistry> GetAll() { return _repository.GetAll(); }
        public List<MaintenanceRegistry> GetByOrderId(int id)
        {
            return _repository.GetAll()
                .Where(entity => entity.OrderFkId == id)
                .ToList();
        }
        public List<MaintenanceRegistry> GetByVehicleId(int id)
        {
            return _repository.GetAll()
                .Where(entity => entity.VehicleFkId == id)
                .ToList();
        }
    }
}
