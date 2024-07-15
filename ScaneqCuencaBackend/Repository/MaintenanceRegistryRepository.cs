using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;

namespace ScaneqCuencaBackend.Repository
{
    public class MaintenanceRegistryRepository : IBaseRepository<MaintenanceRegistry>
    {
        private readonly SeqcuencabackendContext _db;
        public MaintenanceRegistryRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }

        public List<MaintenanceRegistry> GetAll()
        {
            return _db.MaintenanceRegistries.ToList();
        }
        public MaintenanceRegistry? GetById(int id)
        {
            return _db.MaintenanceRegistries.FirstOrDefault(entity => entity.Id == id);
        }

        public List<MaintenanceRegistry> AddMultiple(List<MaintenanceRegistry> registries)
        {
            try
            {
                foreach (var maintenance in registries)
                {
                    _db.MaintenanceRegistries.Add(maintenance);
                }

                _db.SaveChanges();

                return registries;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public MaintenanceRegistry? Add(MaintenanceRegistry entity)
        {
            throw new NotImplementedException();
        }

        public MaintenanceRegistry? Update(MaintenanceRegistry entity)
        {
            throw new NotImplementedException();
        }

        public MaintenanceRegistry? Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MaintenanceRegistry> UpdateOrderRegistries(int id, List<MaintenanceRegistry> registries)
        {
            _db.MaintenanceRegistries.Where(entity => entity.OrderFkId == id)
                .ExecuteDelete();

            if (registries.Count <= 0)
            {
                return null;
            }

            foreach (var registry in registries)
            {
                _db.MaintenanceRegistries.Add(registry);
            }

            _db.SaveChanges();
            return registries;
        }
    }
}
