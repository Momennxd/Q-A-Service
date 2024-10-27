﻿using Core.Services.Interfaces;
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

        public async Task<SendUserDTO?> CreateUserAsync(UsersDTOs.AddUserDTO addUserDTO)
        {
            var user = _mapper.Map<User>(addUserDTO);
            await _unitOfWork.EntityRepo.AddItemAsync(user);
            if (await _unitOfWork.CompleteAsync() < 1) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(addUserDTO.Person);

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



        public async Task<UsersDTOs.AddUserDTO?> GetUserByIdAsync(int UserID)
        {
            var user = await _unitOfWork.EntityRepo.GetUserByID(UserID);

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(user.Person);

            return sendDto;
        }


        public async Task<AddUserDTO?> GetUserByUsernameAsync(string Username)
        {
            var user = await _unitOfWork.EntityRepo.GetUserUsernameAsync(Username);

            if (user == null) return null;

            //getting the send user dto
            var sendDto = _mapper.Map<SendUserDTO>(user);

            //getting the person send dto 
            sendDto.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(user.Person);

            return sendDto;
        }
        public async Task<List<UsersDTOs.SendUserDTO>> GetTopUsersAsync(int topN = 10)
        {
            // Await the asynchronous call to retrieve users
            var users = await _unitOfWork.EntityRepo.GetTopUsersAsync(topN);

            // Map the retrieved users to the DTOs
            var userDtos = _mapper.Map<List<UsersDTOs.SendUserDTO>>(users);

            // Return the mapped DTOs
            return userDtos;
        }

    }

}
