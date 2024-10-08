using Core.Services.Interfaces;
using UoW.Unit_Of_Work;
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

namespace Core.Services.Concrete.Users
{
    public class UserService : IUserService
    {


        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IUserRepo, User> _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork<IUserRepo, User> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> CreateUserAsync(UsersDTOs.AddUserDTO addUserDTO)
        {
            var user = _mapper.Map<User>(addUserDTO);
            await _unitOfWork.EntityRepo.AddItemAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await _unitOfWork.EntityRepo.DeleteItemAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<User?> GetUser(int UserID)
        {
            var user = await _unitOfWork.EntityRepo.FindAsync(UserID);
            return user;
        }

        public async Task<UsersDTOs.AddUserDTO?> GetUserByIdAsync(int UserID)
        {
            var user = await _unitOfWork.EntityRepo.GetUserByID(UserID);

            return _mapper.Map<AddUserDTO>(user);
        }

        public async Task<User?> Login(UsersDTOs.LoginDTO loginDTO)
        {
            var user = await _unitOfWork.EntityRepo.LoginAsync(loginDTO.Username, loginDTO.Password);

            return user;
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

            // Return the updated collection as a DTO
            return _mapper.Map<SendUserDTO>(entity);
        }




    }

}
