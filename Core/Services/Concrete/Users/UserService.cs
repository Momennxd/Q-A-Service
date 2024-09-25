using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Core_Layer.AppDbContext;
using Core_Layer.models.People;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(ILogger<IUnitOfWork> logger, AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(logger, context);
        }

        //public async Task<User> GetUserByIdAsync(int id)
        //{
        //    // Business logic: Fetch user by ID, including related data if necessary
        //    var user = await _unitOfWork.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        throw new KeyNotFoundException("User not found.");
        //    }
        //    return user;
        //}

        //public async Task<User> GetUserByUsernameAsync(string Username)
        //{
        //    // Business logic: Fetch user by ID, including related data if necessary
        //    var user = await _unitOfWork.Users.FindUserByUsernameAsync(Username);
        //    if (user == null)
        //    {
        //        throw new KeyNotFoundException("User not found.");
        //    }
        //    return user;
        //}

        //public async Task<IEnumerable<User>> GetAllUsersAsync()
        //{
        //    return await _unitOfWork.Users.GetAllItemsAsync();
        //}

        //public async Task<User> CreateUserAsync(User user)
        //{
        //    // Business logic: Add any necessary validation
        //    if (string.IsNullOrEmpty(user.Username))
        //    {
        //        throw new ArgumentException("User email is required.");
        //    }

        //    await _unitOfWork.Users.AddItemAsync(user);
        //    await _unitOfWork.CompleteAsync();
        //    return user;
        //}

        //public async Task UpdateUserAsync(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task DeleteUserAsync(int id)
        //{
        //    throw new NotImplementedException();

        //}
    }

}
