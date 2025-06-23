using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepo : IRepository<User>
    {
        Task<User?> LoginAsync(string Username, string Password);
        Task<SP_GetUser?> GetUser(int UserID);
        Task<SP_GetUser?> GetUser(string email, string fullName, CancellationToken cancellation = default);
        Task<User?> GetUser(string Username);
        Task<List<User>> GetTopUsersAsync(int topN);
    }
}
