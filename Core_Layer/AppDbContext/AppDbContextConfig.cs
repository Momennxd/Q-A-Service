using Microsoft.EntityFrameworkCore;

namespace Core_Layer.AppDbContext
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }



    }
}
