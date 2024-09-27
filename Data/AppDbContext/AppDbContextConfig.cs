using Data.models._SP_;
using Microsoft.EntityFrameworkCore;

namespace Data.DatabaseContext
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SPCollectionCetagory>().HasNoKey(); // Indicate that this is a keyless entity
        }

    }
}
