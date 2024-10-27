using Data.models.People;
using Microsoft.AspNetCore.JsonPatch;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {



        //Task<IEnumerable<User>> GetAllUsersAsync();
        Task<SendUserDTO?> CreateUserAsync(AddUserDTO addUserDTO);
        Task<SendUserDTO?> Login(LoginDTO loginDTO);
        Task<SendUserDTO?> GetUser(int UserID);
        Task<SendUserDTO> PatchUser(JsonPatchDocument<AddUserDTO> patchDoc, int UserID);
        Task<SendUserDTO?> GetUserByIdAsync(int UserID);
        Task<SendUserDTO?> GetUserByUsernameAsync(string UserID);
        Task<bool> DeleteUserAsync(int id);
    }

}
