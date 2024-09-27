
using Data.DatabaseContext;
using Data.models.People;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.Repositories
{
    public class UserRepo : Repository<User>, IUserRepo
    {



        public UserRepo(AppDbContext context) : base(context)
        {
            
        }

        public Task<User?> FindUserByUsernameAsync(string Username)
        {
            return null;
        }

       
    }
}
