using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class BusOrdersRepository
    {
        private readonly DbScaniaCuencaContext _db;
        public BusOrdersRepository(DbScaniaCuencaContext db)
        {
            _db = db;
        }
        public BusOrder getWorkOrderByNumber(int id)
        {
            return _db.BusOrders.Where(x => x.Fid == id).First();
        }

        public List<BusOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.BusOrders.Where(x => x.CustomerId == id).ToList();
        }
    }
}
