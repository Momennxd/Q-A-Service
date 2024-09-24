using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.models.People;


namespace Data.Repositories
{
    public interface IUserRepo : IRespository<User>
    {



        Task<User> FindUserByUsernameAsync(string Username);









    }
}
