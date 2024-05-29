using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class CustomerBll
    {
        private readonly SeqcuencabackendContext _context;
        private readonly CustomerRepository _customerR;
        private readonly VehicleRepository _vehicleR;
        public CustomerBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _context = db;
            _customerR = new CustomerRepository(db);
            _vehicleR = new VehicleRepository(db, mapper);
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
            CustomerResponseModel response = new ()
            {
                Id = result.Id,
                Name = result.Name
            };

            return response;
        }

        public List<Vehicle> getCustomerVehicles(int id)
        {
            List<Vehicle> vehicles = _vehicleR.getVehiclesByCustomerId(id);

            return vehicles;
        }
    }
}