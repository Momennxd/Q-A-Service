
using Core_Layer.Entities.Countries;
using Core_Layer.Entities.People;
using Microsoft.EntityFrameworkCore;

namespace Core_Layer.AppDbContext
{
    public partial class AppDbContext
    {


        public DbSet<eCountryDA> Countries { get; set; }

        public DbSet<eUsersDA> Users { get; set; }
        public DbSet<ePersonDA> People { get; set; }






    }
}
