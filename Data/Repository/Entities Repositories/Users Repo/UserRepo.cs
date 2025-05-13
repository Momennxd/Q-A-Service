
using Data.DatabaseContext;
using Data.models.People;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepo(AppDbContext context, IPasswordHasher<User> passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public Task<User?> FindUserByUsernameAsync(string Username)
        {
           throw new NotImplementedException();
        }

        public async Task<List<User>> GetTopUsersAsync(int topN)
        {
            return await _context.Users
                      .FromSqlRaw("EXEC dbo.GetTopNUsers @TopN = {0}", topN)
                      .ToListAsync();


        }

        public async Task<SP_GetUser?> GetUserByID(int userID)
        {
            var users = await _context.Set<SP_GetUser>()
                .FromSqlInterpolated($"EXEC SP_GetUser @UserID = {userID}")
                .AsNoTracking()
                .ToListAsync();

            return users.FirstOrDefault();
        }




        public async Task<User?> GetUserUsernameAsync(string Username)
        {
            var user = await _context
                .Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Username == Username);

            return user;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }



    }
}
