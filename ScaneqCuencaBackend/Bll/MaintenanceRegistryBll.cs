using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class MaintenanceRegistryBll
    {
        private readonly SeqcuencabackendContext _db;
        private readonly MaintenanceRegistryRepository _repositoryMaintenance;
        public MaintenanceRegistryBll(SeqcuencabackendContext db)
        {
            _db = db;
            _repositoryMaintenance = new(db);

        }

        public List<MaintenanceRegistry> GetAll() { return _repositoryMaintenance.GetAll(); }
        public List<MaintenanceRegistry> GetByOrderId(int id)
        {
            return _repositoryMaintenance.GetAll()
                .Where(entity => entity.OrderFkId == id)
                .ToList();
        }
        public List<MaintenanceRegistry> GetByVehicleId(int id)
        {
            return _repositoryMaintenance.GetAll()
                .Where(entity => entity.VehicleFkId == id)
                .GroupBy(entity => entity.MaintenanceType)
                .Select(group => group.OrderByDescending(entity => entity.Kilometers).First())
                .ToList();
        }

        public List<MaintenanceRegistry> DeleteAllOrdersByWorkOrderId(int id)
        {
            try
            {
                var result = GetByOrderId(id);
                foreach (var order in result)
                {
                    _db.Remove(order);
                }

                _db.SaveChanges();
                return result;
            } catch (Exception)
            {
                return null;
            }       
        }
    }
}
