using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.models.People;
using Core.DTOs.People;
using AutoMapper;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using static Core.DTOs.People.UsersDTOs;
using Core.DTOs.Collections;
using Data.Repository.Entities_Repositories.People_Repo;
using static Core.DTOs.People.PeopleDTOs;
using Microsoft.AspNetCore.Identity;
using Data.Repository.Entities_Repositories.RefreshTokens_Repo;
using Data.models.RefreshTokens;
using Core.Services.Interfaces.RefreshTokens;

namespace Core.Services.Concrete.Users
{
    public class UserService : IUserService
    {

        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IUserRepo, User> _unitOfWork;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserService(IPasswordHasher<User> passwordHasher, 
            IMapper mapper, 
            IUnitOfWork<IUserRepo, User> unitOfWork,
            IRefreshTokenService refreshTokenService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<SendUserDTO?> CreateUserAsync(UsersDTOs.AddUserDTO addUserDTO)
        {
            var user = _mapper.Map<User>(addUserDTO);
            user.Password = _passwordHasher.HashPassword(user, addUserDTO.Password);
            await _unitOfWork.EntityRepo.AddItemAsync(user);
            if (await _unitOfWork.CompleteAsync() < 1) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<SendPersonDTO>(user.Person);

            return sendDto; 
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await _unitOfWork.EntityRepo.DeleteItemAsync(id);
            if (await _unitOfWork.CompleteAsync() < 1) return false;
            return true;
        }

        public async Task<SendUserDTO?> GetUser(int UserID)
        {
            var user = await _unitOfWork.EntityRepo.FindAsync(UserID);

            if (user == null) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(user.Person);

            return sendDto;
        }




        public async Task<SendUserDTO?> Login(UsersDTOs.LoginDTO loginDTO)
        {
            var user = await _unitOfWork.EntityRepo.LoginAsync(loginDTO.Username, loginDTO.Password);

            if (user == null) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(user.Person);

            return sendDto;
        }



        public async Task<SendUserDTO> PatchUser(JsonPatchDocument<AddUserDTO> patchDoc, int UserID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _unitOfWork.EntityRepo.FindAsync(UserID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"User with ID {UserID} not found.");
            }



            // Map the entity to a DTO to apply the patch
            var UserDTOToPatch = _mapper.Map<AddUserDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(UserDTOToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(UserDTOToPatch, entity);

            // Save changes
            await _unitOfWork.CompleteAsync();

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(entity);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(entity.Person);

            return sendDto;
        }



        public async Task<GetUserDTO?> GetUserByIdAsync(int UserID)
        {
            var user = await _unitOfWork.EntityRepo.GetUser(UserID);

            //getting the send user dto
            var sendDto = _mapper.Map<GetUserDTO>(user);


            return sendDto;
        }


        public async Task<SendUserDTO?> GetUserByUsernameAsync(string Username)
        {
            var user = await _unitOfWork.EntityRepo.GetUser(Username);

            if (user == null) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(user.Person);

            return sendDto;
        }
        public async Task<List<SendUserDTO>> GetTopUsersAsync(int topN = 10)
        {
            // Await the asynchronous call to retrieve users
            var users = await _unitOfWork.EntityRepo.GetTopUsersAsync(topN);

            // Map the retrieved users to the DTOs
            var userDtos = _mapper.Map<List<UsersDTOs.SendUserDTO>>(users);

            // Return the mapped DTOs
            return userDtos;
        }



        private async Task<GetUserDTO> _GetUser_ExternalAuth(string email, string fullName)
        {
            var user = await _unitOfWork.EntityRepo.GetUser(email, fullName);
            var userDto = _mapper.Map<GetUserDTO>(user);
            return userDto;
        }


        public async Task<ExternalAuthResponseDTO?> GetExternalAuthResponse(string email, string fullName)
        {
            var user = await _GetUser_ExternalAuth(email, fullName);
            if (user == null) return null;
            var tokens = await _refreshTokenService.GenerateTokensForUserAsync(user.UserId);
            await _unitOfWork.CompleteAsync();
            return new ExternalAuthResponseDTO
            {
                user = user,
                tokens = tokens
            };
        }

    }

}
