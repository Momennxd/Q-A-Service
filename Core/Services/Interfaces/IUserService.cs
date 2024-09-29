using Core.DTOs.People;
using Data.models.People;
using Microsoft.AspNetCore.JsonPatch;
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



        //Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> CreateUserAsync(AddUserDTO addUserDTO);
        Task<User?> Login(LoginDTO loginDTO);
        Task<User?> GetUser(int UserID);
        Task<User?> PatchUserAsync(JsonPatchDocument<User> UpdatedItem, dynamic PrimaryKey);
        Task<AddUserDTO?> GetUserByIdAsync(int UserID);
        Task<bool> DeleteUserAsync(int id);
    }

}
