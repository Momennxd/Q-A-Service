
using Data.DatabaseContext;
using Data.models.People;
using Microsoft.EntityFrameworkCore;
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

        AppDbContext _context;

        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<User?> FindUserByUsernameAsync(string Username)
        {
            return null;
        }

        public async Task<User?> LoginAsync(string Username, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Username == Username && u.Password == Password);

            return user;
        }
    }
}
