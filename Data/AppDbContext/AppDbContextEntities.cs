
using Data.models._SP_;
using Data.models.Collections;
using Data.models.People;
using Data.models.Pictures;
using Data.models.Questions;
using Data.models.Institutions;
using Microsoft.EntityFrameworkCore;
namespace Data.DatabaseContext
{
    public partial class AppDbContext
    {
       public virtual DbSet<Chosen_Choices> Chosen_Choices { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }

        public virtual DbSet<Collections_Questions> Collections_Questions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<SP_Question> sp_Question { get; set; }

        public virtual DbSet<Categories> Categories { get; set; }

        public virtual DbSet<Collections_Likes> Collections_Likes { get; set; }

        public virtual DbSet<CollectionsCategories> Collections_Categories { get; set; }

        public virtual DbSet<QuestionsChoices> Questions_Choices { get; set; }


        public virtual DbSet<User> Users {  get; set; }

 
        public virtual DbSet<Person> People { get; set; }


        public virtual DbSet<QCollection> QCollections { get; set; }
        public virtual DbSet<AnswerExplanation> Questions_Answer_Explanation { get; set; }

        public virtual DbSet<Choices_Pics> Choices_Pics { get; set; }
        public virtual DbSet<Pics> Pics { get; set; }

        public virtual DbSet<SPCollectionCetagory> sp_CollectionCetagories { get; set; }
        public virtual DbSet<SP_IsRightAnswersAccess> sp_HasRightAnswersAccess { get; set; }

    }
}
