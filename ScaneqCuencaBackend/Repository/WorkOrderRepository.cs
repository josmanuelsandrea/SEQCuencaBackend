using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class WorkOrderRepository
    {
        private readonly DbScaniaCuencaContext _db;
        public WorkOrderRepository(DbScaniaCuencaContext db)
        {
            _db = db;
        }
        public WorkOrder getWorkOrderByNumber(int id)
        {
            return _db.WorkOrders.Where(x => x.Fid == id).First();
        }

        public List<WorkOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.WorkOrders.Where(x => x.CustomerId == id).ToList();
        }
    }
}
