using Data.models.People;
using Microsoft.AspNetCore.JsonPatch;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {



        //Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> CreateUserAsync(AddUserDTO addUserDTO);
        Task<User?> Login(LoginDTO loginDTO);
        Task<User?> GetUser(int UserID);
        Task<SendUserDTO> PatchUser(JsonPatchDocument<AddUserDTO> patchDoc, int UserID);
        Task<AddUserDTO?> GetUserByIdAsync(int UserID);
        Task<AddUserDTO?> GetUserByUsernameAsync(string UserID);
        Task<bool> DeleteUserAsync(int id);
    }

}
