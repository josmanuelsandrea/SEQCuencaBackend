using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

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

        public Customer? CreateCustomer(CustomerRequestModel model)
        {
            Customer createdCustomer = new() { Name = model.Name };
            try
            {
                _db.Customers.Add(createdCustomer);
                _db.SaveChanges();
                return createdCustomer;
            } catch (Exception)
            {
                return null;
            }
        }
    }
}
