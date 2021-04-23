using App.Entities;
using App.Entities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BusinessLogicContracts
{
    public interface IUserService
    {
        MethodResult<User> GetByEmail(string email);

        MethodResult<User> GetById(int userId);

        MethodResult<User> Register(string email, string fullName, string address, string phone, string password, string confirmPassword);

        MethodResult<User> FindByEmailAndPassword(string email, string password);

        MethodResult<User> UpdateUser(int userId, string fullName, string address, string phone);

        VoidResult DeleteUser(int userId); 
    }
}
