using App.BusinessLogicContracts;
using App.DataAccessContracts;
using App.Entities;
using App.Entities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public MethodResult<User> GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                return new MethodResult<User>(user);
            }
            return MethodResult<User>.CreateFaultedResult("The user can't be found.");
        }

        public MethodResult<User> GetById(int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user != null)
            {
                return new MethodResult<User>(user);
            }

            return MethodResult<User>.CreateFaultedResult("The user can't be found.");
        }

        public MethodResult<User> UpdateUser(int userId, string fullName, string address, string phone)
        {
            var updateUser = _userRepository.GetById(userId);

            if (updateUser != null)
            {
                updateUser.FullName = fullName;
                updateUser.Address = address;
                updateUser.Phone = phone;

                _userRepository.Update(updateUser);

                return new MethodResult<User>(updateUser);
            }
            else
            {
                return MethodResult<User>.CreateFaultedResult("The user can't be found.");
            }
           
        }
        public VoidResult DeleteUser(int userId)
        {
            var deleteUser = _userRepository.GetById(userId);
            if(deleteUser != null)
            {
                _userRepository.Delete(deleteUser.Id);
                return VoidResult.Ok();
            }
            else
            {
                return VoidResult.CreateFaultedResult("The user can't be found.");
            }
        }

        public MethodResult<User> FindByEmailAndPassword(string email, string password)
        {
            var users = _userRepository.GetAll();
            var user = users.FirstOrDefault(x => x.Email == email && x.Password == EncryptPassword(password));
            
            if(user == null)
            {
                return MethodResult<User>.CreateFaultedResult("The user does not exist!");
            }

            return new MethodResult<User>(user);
        }

        public MethodResult<User> Register(string email, string fullName, string address, string phone, string password, string confirmPassword)
        {
            // Check rules
            var existingUser = _userRepository.GetByEmail(email);
            if (existingUser != null)
            {
                return MethodResult<User>.CreateFaultedResult("The email is taken.");
            }

            if (password != confirmPassword)
            {
                return MethodResult<User>.CreateFaultedResult("Password and Confirm Password do not match.");
            }

            if (password.Length >= 1 && password.Length < 6)
            {
                return MethodResult<User>.CreateFaultedResult("The password is too short."); 
            }

            // Create user
            var user = new User
            {
                FullName = fullName,
                Email = email,
                Address = address,
                Phone = phone,
                Password = EncryptPassword(password)
            };

            // Save the user
            _userRepository.Add(user);

            // Return the user
            return new MethodResult<User>(user);
        }

        private string EncryptPassword(string password)
        {
            var encode = Encoding.UTF8.GetBytes(password);
            var encryptPassword = Convert.ToBase64String(encode);
            return encryptPassword;
        }
    }
}
