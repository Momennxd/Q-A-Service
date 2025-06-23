
using Data.DatabaseContext;
using Data.models.People;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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


        public async Task<List<User>> GetTopUsersAsync(int topN)
        {
            return await _context.Users
                      .FromSqlRaw("EXEC dbo.GetTopNUsers @TopN = {0}", topN)
                      .ToListAsync();


        }

        public async Task<SP_GetUser?> GetUser(int userID)
        {
            var users = await _context.Set<SP_GetUser>()
                .FromSqlInterpolated($"EXEC SP_GetUser @UserID = {userID}")
                .AsNoTracking()
                .ToListAsync();

            return users.FirstOrDefault();
        }




        public async Task<User?> GetUser(string Username)
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
        public override Task<EntityEntry> AddItemAsync(User user)
        {

            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            return base.AddItemAsync(user);
        }
        private async Task<User> _CreateUserAsync(string email, string fullName, CancellationToken cancellationToken = default)
        {
            string firstname = fullName.Contains(' ') ? fullName.Substring(0, fullName.IndexOf(' ')) : fullName;
            string lastname = fullName.Contains(' ') ? fullName.Substring(fullName.IndexOf(' ') + 1) : "";
            string username = email.Split('@')[0].Replace(".", "");
            var newUser = new User
            {
                Username = username,
                Password = Guid.NewGuid().ToString(),
                Person = new Person
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email
                }
            };
            await AddItemAsync(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            return newUser;
        }
        public async Task<SP_GetUser?> GetUser(string email, string fullName, CancellationToken cancellationToken = default)
        {
            var user = (await _context.Set<SP_GetUser>()
                .FromSqlInterpolated($"EXEC SP_GetUserByEmail @Email = {email}")
                .AsNoTracking()
                .ToListAsync(cancellationToken))
                .FirstOrDefault();


            if (user != null)
                return user;

            await _CreateUserAsync(email, fullName, cancellationToken);
            var createdUser = (await _context.Set<SP_GetUser>()
                .FromSqlInterpolated($"EXEC SP_GetUserByEmail @Email = {email}")
                .AsNoTracking()
                .ToListAsync(cancellationToken))
                .FirstOrDefault();


            return createdUser;
        }

    }
}
