using Data.models._SP_;
using Data.models.Collections;
using Data.models.People;
using Data.models.Questions;
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
            modelBuilder.Entity<SP_QuestionCategories>().HasNoKey(); // Indicate that this is a keyless entity
            modelBuilder.Entity<SP_GetRandomQuestion>().HasNoKey().ToView(null);
            modelBuilder.Entity<SP_GetUser>().HasNoKey();
            modelBuilder.Entity<SP_RightChoiceWithExplanation>().HasNoKey().ToView(null);

            modelBuilder
                .Entity<CollectionSubmissionView>()
                .HasNoKey()
                .ToView("View_CollectionSubmissions");



        }


    }
}
