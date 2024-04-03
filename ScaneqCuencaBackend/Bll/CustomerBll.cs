using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class CustomerBll
    {
        private readonly DbScaniaCuencaContext _context;
        private readonly CustomerRepository _customerR;
        public CustomerBll(DbScaniaCuencaContext db)
        {
            _context = db;
            _customerR = new CustomerRepository(db);
        }
        public List<CustomerResponseModel> getAllCustomers()
        {
            List<Customer> result = _customerR.getAllCustomers();
            List<CustomerResponseModel> response = new ();
            foreach (Customer customer in result)
            {
                response.Add(new CustomerResponseModel()
                {
                    Id = customer.Id,
                    Name = customer.Name
                });
            };

            return response;
        }
        public CustomerResponseModel getCustomerById(int id)
        {
            Customer? result = _customerR.getCustomerById(id);
            CustomerResponseModel response = new CustomerResponseModel()
            {
                Id = result.Id,
                Name = result.Name
            };

            return response;
        }
    }
}