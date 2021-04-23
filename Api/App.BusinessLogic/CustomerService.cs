using App.BusinessLogicContracts;
using App.DataAccessContracts;
using App.Entities;
using App.Entities.Results;
using System.Collections.Generic;

namespace App.BusinessLogic
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public MethodResult<List<Customer>> GetAllForUser(int userId)
        {
            var customers = _customerRepository.GetAllForUser(userId);
            return new MethodResult<List<Customer>>(customers);
        }

        public MethodResult<Customer> GetById(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);

            if (customer != null)
            {
                return new MethodResult<Customer>(customer);
            }

            return MethodResult<Customer>.CreateFaultedResult("The customer with this Id can't be found.");
        }

        public MethodResult<Customer> GetByEmailForUser(string email, int userId)
        {
            var customer = _customerRepository.GetByEmailForUser(email, userId);

            if (customer != null)
            {
                return new MethodResult<Customer>(customer);
            }

            return MethodResult<Customer>.CreateFaultedResult("The customer with this email can't be found.");
        }

        public MethodResult<Customer> AddCustomer(string name, string email, string address, string city, string phone, int userId)
        {
            var existingCustomer = _customerRepository.GetByEmailForUser(email, userId);

            if (existingCustomer != null)
            {
                return MethodResult<Customer>.CreateFaultedResult("The Email is taken for the user.");
            }

            var newCustomer = new Customer
            {
                Name = name,
                Email = email,
                Address = address,
                City = city,
                Phone = phone,
                UserId = userId
            };

            _customerRepository.Add(newCustomer);

            return new MethodResult<Customer>(newCustomer);
        }

        public MethodResult<Customer> UpdateCustomer(int customerId, string name, string address, string city, string phone)
        {
            var updateCustomer = _customerRepository.GetById(customerId);

            if (updateCustomer != null)
            {
                updateCustomer.Name = name;
                updateCustomer.Address = address;
                updateCustomer.City = city;
                updateCustomer.Phone = phone;

                _customerRepository.Update(updateCustomer);

                return new MethodResult<Customer>(updateCustomer);
            }
            else
            {
                return MethodResult<Customer>.CreateFaultedResult("The customer with this Id can't be found.");
            }
        }

        public VoidResult DeleteCustomer(int customerId)
        {
            var deleteCustomer = _customerRepository.GetById(customerId);

            if (deleteCustomer != null)
            {
                _customerRepository.Delete(deleteCustomer.Id);
                return VoidResult.Ok();
            }
            else
            {
                return MethodResult<Customer>.CreateFaultedResult("The customer with this Id can't be found.");
            }
        }
    }
}
