
using Data.models._SP_;
using Data.models.Collections;
using Data.models.People;
using Data.models.Pictures;
using Data.models.Questions;
using Microsoft.EntityFrameworkCore;

namespace Data.DatabaseContext
{
    public partial class AppDbContext
    {

        public virtual DbSet<CollectionsCategories> Collections_Categories { get; set; }

        public virtual DbSet<QuestionsChoices> Questions_Choices { get; set; }


        public virtual DbSet<User> Users {  get; set; }

        public virtual DbSet<Pics> Pics { get; set; }


        public virtual DbSet<Person> People { get; set; }


        public virtual DbSet<QCollection> QCollections { get; set; }


        public virtual DbSet<SPCollectionCetagory> SpCollectionCetagories { get; set; }

    }
}
