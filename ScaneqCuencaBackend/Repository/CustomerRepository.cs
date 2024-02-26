using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class CustomerRepository
    {
        private readonly static DbScaniaCuencaContext _db = new DbScaniaCuencaContext();

        public static List<Customer> getAllCustomers()
        {
            return _db.Customers.ToList();
        }

        public static Customer? getCustomerById(int id)
        {
            return _db.Customers.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
