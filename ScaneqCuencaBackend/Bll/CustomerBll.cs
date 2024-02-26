using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public static class CustomerBll
    {
        public static List<CustomerResponseModel> getAllCustomers()
        {
            List<Customer> result = CustomerRepository.getAllCustomers();
            List<CustomerResponseModel> response = new List<CustomerResponseModel>();
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
        public static CustomerResponseModel getCustomerById(int id)
        {
            Customer result = CustomerRepository.getCustomerById(id);
            CustomerResponseModel response = new CustomerResponseModel()
            {
                Id = result.Id,
                Name = result.Name
            };

            return response;
        }
    }
}