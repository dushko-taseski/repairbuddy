using App.DataAccessContracts;
using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var result = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
    }
}
