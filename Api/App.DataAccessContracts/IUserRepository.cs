using App.Entities;
using System.Collections.Generic;


namespace App.DataAccessContracts
{
    public interface IUserRepository
    {
        List<User> GetAll();

        void Add(User user);

        void Update(User user);

        void Delete(int userId);

        User GetByEmail(string email);

        User GetById(int userId);
    }
}
