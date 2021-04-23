using App.DataAccessContracts;
using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataBaseContext _context;

        public CustomerRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(int customerId)
        {
            var result = _context.Customers.FirstOrDefault(x => x.Id == customerId);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }

        public List<Customer> GetAllForUser(int userId)
        {
            return _context.Customers.Where(x => x.UserId == userId).ToList();
        }

        public Customer GetByEmailForUser(string email, int userId)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email && x.UserId == userId);
        }

        public Customer GetById(int customerId)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == customerId);
        }
    }
}
