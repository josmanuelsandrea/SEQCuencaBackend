using ScaneqCuencaBackend.DBModels;

namespace ScaneqCuencaBackend.Repository
{
    public class CustomerRepository
    {
        private readonly DbScaniaCuencaContext _db;
        public CustomerRepository(DbScaniaCuencaContext context)
        {
            _db = context;
        }

        public List<Customer> getAllCustomers()
        {
            return _db.Customers.ToList();
        }

        public Customer? getCustomerById(int id)
        {
            return _db.Customers.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
