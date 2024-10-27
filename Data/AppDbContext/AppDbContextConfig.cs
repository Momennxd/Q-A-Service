using Data.models._SP_;
using Data.models.Collections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Data.DatabaseContext
{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // This method has the `async` modifier, so `await` can be used:
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ValidateEntities(); // Run validation before saving changes
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public override int SaveChanges()
        {
            ValidateEntities(); // Run validation before saving changes
            return base.SaveChanges();
        }

        private void ValidateEntities()
        {
            // Validate all entities that are added or modified
            var entitiesToValidate = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entitiesToValidate)
            {
                var validationContext = new ValidationContext(entity);
                var validationResults = new List<ValidationResult>();

                // This will call the IsValid method on each custom attribute, including MinStringLength
                bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true);

                if (!isValid)
                {
                    var messages = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                    throw new ValidationException($"Validation failed for {entity.GetType().Name}: {messages}");
                }
            }
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
