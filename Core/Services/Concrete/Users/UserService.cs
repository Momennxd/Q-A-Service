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

        public async Task<User?> PatchUserAsync(JsonPatchDocument<User> UpdatedItem, dynamic PrimaryKey)
        {
            var user = await _unitOfWork.EntityRepo.PatchItemAsync(UpdatedItem, PrimaryKey);
            await _unitOfWork.CompleteAsync();
            return user;
        }
    
    
    }

}
