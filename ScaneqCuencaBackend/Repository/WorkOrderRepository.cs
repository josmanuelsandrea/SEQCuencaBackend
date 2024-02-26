using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public static class WorkOrderRepository
    {
        private readonly static DbScaniaCuencaContext _db = new DbScaniaCuencaContext();
        public static WorkOrder getWorkOrderByNumber(int id)
        {
            return _db.WorkOrders.Where(x => x.Fid == id).First();
        }

        public static List<WorkOrder> getAllWorkOrdersByCustomerId(int id)
        {
            return _db.WorkOrders.Where(x => x.CustomerId == id).ToList();
        }
    }
}
