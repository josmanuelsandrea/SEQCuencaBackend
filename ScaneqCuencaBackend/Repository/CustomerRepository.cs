﻿using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Repository
{
    public class CustomerRepository
    {
        private readonly SeqcuencabackendContext _db;
        public CustomerRepository(SeqcuencabackendContext context)
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

        public Customer? UpdateCustomer(Customer model)
        {
            try
            {
                var foundCustomer = _db.Customers.Where(entity => entity.Id == model.Id).FirstOrDefault();
                if (foundCustomer == null)
                {
                    return null;
                }

                _db.Entry(foundCustomer).CurrentValues.SetValues(model);
                _db.SaveChanges();

                return foundCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
