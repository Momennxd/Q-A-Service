using Core.DTOs.People;
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
        //Task<GetUserDTO?> GetUser(int UserID);
        Task<SendUserDTO> PatchUser(JsonPatchDocument<AddUserDTO> patchDoc, int UserID);
        Task<GetUserDTO?> GetUserByIdAsync(int UserID);
        Task<SendUserDTO?> GetUserByUsernameAsync(string UserID);
        Task<bool> DeleteUserAsync(int id);
        Task<List<SendUserDTO>> GetTopUsersAsync(int topN = 10);
        Task<ExternalAuthResponseDTO?> GetExternalAuthResponse(string email, string fullName);

    }

}
