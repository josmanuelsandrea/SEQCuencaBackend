using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class TruckOrdersRepository
    {
        private readonly SeqcuencabackendContext _db;
        public TruckOrdersRepository(SeqcuencabackendContext db)
        {
            _db = db;
        }
        public TruckOrder getWorkOrderByNumber(int id)
        {
            return _db.TruckOrders.Where(x => x.Fid == id).First();
        }

        public List<TruckOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.TruckOrders.Where(x => x.CustomerId == id).ToList();
        }

        public List<TruckOrder> getWorkOrderByVehicleId(int id)
        {
            return _db.TruckOrders.Where(x => x.VehicleId == id).ToList();
        }
    }
}
