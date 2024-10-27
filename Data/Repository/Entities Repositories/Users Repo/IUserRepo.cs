﻿using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepo : IRepository<User>
    {



        Task<User> FindUserByUsernameAsync(string Username);


        Task<User?> LoginAsync(string Username, string Password);



        Task<User?> GetUserByID(int UserID);
        Task<User?> GetUserUsernameAsync(string Username);

        Task<List<User>> GetTopUsersAsync(int topN);
    }
}
