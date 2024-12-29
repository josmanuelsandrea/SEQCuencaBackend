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

        public CustomerBll(IMapper mapper, VehicleRepository vehicleR, CustomerRepository customerR, SeqcuencabackendContext context)
        {
            _vehicleR = vehicleR;
            _customerR = customerR;
            _context = context;
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> result = _customerR.getAllCustomers();

            return result;
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

        public Customer UpdateCustomer(Customer model)
        {
            var response = _customerR.UpdateCustomer(model);
            return response;
        }
    }
}