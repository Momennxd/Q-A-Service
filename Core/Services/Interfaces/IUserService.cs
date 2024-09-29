using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {



        //Task<User> GetUserByIdAsync(int id);
        //Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> CreateUserAsync(AddUserDTO addUserDTO);
        Task<User?> Login(LoginDTO loginDTO);
        //Task UpdateUserAsync(User user);
        //Task DeleteUserAsync(int id);
    }

}
