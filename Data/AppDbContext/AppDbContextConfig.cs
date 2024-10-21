using Data.models._SP_;
using Data.models.Collections;
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
            modelBuilder.Entity<SP_IsRightAnswersAccess>().HasNoKey(); // Indicate that this is a keyless entity
            modelBuilder.Entity<SP_Question>().HasNoKey(); // Indicate that this is a keyless entity

            // Configure the primary key for the join entity
            modelBuilder.Entity<CollectionsCategories>()
                .HasKey(cc => new { cc.CollectionID, cc.CategoryID });

            // Configure the relationship between QCollection and CollectionCategory
            modelBuilder.Entity<CollectionsCategories>()
                .HasOne(cc => cc.QCollection)
                .WithMany(q => q.CollectionCategories)
                .HasForeignKey(cc => cc.CollectionID);

            // Configure the relationship between Category and CollectionCategory
            modelBuilder.Entity<CollectionsCategories>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CollectionCategories)
                .HasForeignKey(cc => cc.CategoryID);
        }


    }
}
