using App.Entities;
using System.Collections.Generic;

namespace App.DataAccessContracts
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllForUser(int userId);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(int customerId);

         Customer GetByEmailForUser(string email, int userId);

         Customer GetById(int customerId);
    }
}
