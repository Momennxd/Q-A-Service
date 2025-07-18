﻿
using Data.models._SP_;
using Data.models.Collections;
using Data.models.People;
using Data.models.Pictures;
using Data.models.Questions;
using Data.models.Institutions;
using Microsoft.EntityFrameworkCore;
using Data.models.nsCategories;
using Data.models.RefreshTokens;
namespace Data.DatabaseContext
{
    public partial class AppDbContext
    {
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Pics> Pics { get; set; }
        public DbSet<CollectionSubmissionView> CollectionSubmissionViews { get; set; }
        public DbSet<SP_GetUser> UserWithPoints { get; set; }
        public DbSet<SP_RightChoiceWithExplanation> RightChoiceWithExplanation { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        #region Choices
        public virtual DbSet<Chosen_Choices> Chosen_Choices { get; set; }
        public virtual DbSet<Choices_Pics> Choices_Pics { get; set; }
        public virtual DbSet<SP_IsRightAnswersAccess> sp_HasRightAnswersAccess { get; set; }
        #endregion

        #region Questions
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<SP_Question> sp_Question { get; set; }
        public virtual DbSet<QuestionsChoices> Questions_Choices { get; set; }
        public virtual DbSet<AnswerExplanation> Questions_Answer_Explanation { get; set; }
        public virtual DbSet<SP_QuestionCategories> SP_QuestionCategories { get; set; }
        public virtual DbSet<Questions_Categories> Questions_Categories { get; set; }
        public DbSet<SP_GetRandomQuestion> FlatQuestionChoices { get; set; }

        #endregion

        #region Collections
        public virtual DbSet<Collections_Submitions> Collections_Submitions { get; set; }
        public virtual DbSet<Collections_Questions> Collections_Questions { get; set; }
        public virtual DbSet<Collections_Reviews> Collections_Reviews { get; set; }
        public virtual DbSet<Collections_Likes> Collections_Likes { get; set; }
        public virtual DbSet<QCollection> QCollections { get; set; }
        public virtual DbSet<SPCollectionCetagory> sp_CollectionCategories { get; set; }
        #endregion
    }
}
