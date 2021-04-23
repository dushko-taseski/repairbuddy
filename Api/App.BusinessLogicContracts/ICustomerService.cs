using App.Entities;
using App.Entities.Results;
using System.Collections.Generic;

namespace App.BusinessLogicContracts
{
    public interface ICustomerService
    {
        MethodResult<List<Customer>> GetAllForUser(int userId);

        MethodResult<Customer> GetById(int customerId);

        MethodResult<Customer> GetByEmailForUser(string email, int userId);

        MethodResult<Customer> AddCustomer(string name, string email, string address, string city, string phone, int userId);

        MethodResult<Customer> UpdateCustomer(int customerId, string name, string address, string city, string phone);

        VoidResult DeleteCustomer(int customerId);
    }
}
