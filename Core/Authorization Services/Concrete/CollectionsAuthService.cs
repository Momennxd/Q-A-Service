using AutoMapper;
using Core.Authorization_Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsSubmitions;
using Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions;
using Data.Repository.Entities_Repositories.Questions_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization_Services.Concrete
{
    public class CollectionsAuthService : ICollectionsAuthService
    {


        private readonly IUnitOfWork<ICollectionRepo, QCollection> _uowCollec;
        private readonly IUnitOfWork<IQuestionRepo, Question> _uowQuestion;
        private readonly IUnitOfWork<ICollectionsQuestionRepo, Collections_Questions> _uowCollecQuestion;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _uowChoices;
        private readonly IUnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions> _uowCollectionsSubmitions;
        private readonly IUnitOfWork<ICollectionsReviewsRepo, Collections_Reviews> _uowCollectionReviews;

        public CollectionsAuthService(IUnitOfWork<ICollectionRepo, QCollection> uowCollections,
            IUnitOfWork<IQuestionRepo, Question> uowQuestion,
            IUnitOfWork<ICollectionsQuestionRepo, Collections_Questions> uowcollectionsQuestion,
            IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowChoices,
            IUnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions> uowCollectionsSubmitions,
            IUnitOfWork<ICollectionsReviewsRepo, Collections_Reviews> uowCollectionReviews)
        {
            _uowCollecQuestion = uowcollectionsQuestion;
            _uowCollec = uowCollections;
            _uowQuestion = uowQuestion;
            _uowChoices = uowChoices;
            _uowCollectionsSubmitions = uowCollectionsSubmitions;
            _uowCollectionReviews = uowCollectionReviews;

        }



        public async Task<bool> IsUserQuestionAccessAsync(int QuestionID, int UserID)
        {
            var question = await _uowQuestion.EntityRepo.FindAsync(QuestionID);
            if (question == null) throw new ArgumentNullException();

            var collectionsQuestions = await _uowCollecQuestion.EntityRepo.GetCollectionQuestionsAsync(QuestionID);
            if (collectionsQuestions == null) throw new ArgumentNullException();

            var collection = await _uowCollec.EntityRepo.FindAsync(collectionsQuestions.CollectionID);
            if (collection == null) throw new ArgumentNullException();

            //if the question is not the users's and the question's collection is private means no access
            return !(question.UserID != UserID && !collection.IsPublic);
        }



        public async Task<bool> IsUserCollecOwnerAsync(int collecID, int userID)
        {

            if (collecID <= 0 || userID <= 0) {  return false; }

            var collection = await _uowCollec.EntityRepo.FindAsync(collecID);

            // Ensure collection is not null and check if the user is the owner
            if (collection == null) return false; 
            
            return collection.CreatedByUserId == userID;
        }

        public async Task<bool> IsUserQuestionOwnerAsync(int QuestionID, int UserID)
        {

            var question  = await _uowQuestion.EntityRepo.FindAsync(QuestionID);

            if (question == null) return false;

            return question.UserID == UserID;

        }

        public async Task<bool> IsRightsAnswersAccessAsync(int QuestionID, int UserID)
        {

            //authorization:
            //1- if the caller is the creater, then no auth needed.
            //2- if the caller is the consumer, then the consumer must answer the question first to call the API
            //to prevent asnwers leak.

            return await _uowQuestion.EntityRepo.IsUserRightAnswerAccessAsync(QuestionID, UserID);

        }

        public async Task<bool> IsUserQuestionAccessAsync(HashSet<int> setQuestionIDs, int UserID)
        {
            foreach (var id in setQuestionIDs)
                if (!await IsUserQuestionAccessAsync(id, UserID)) return false;

            return true;
        }

        public async Task<bool> IsUserCollectionAccess(int CollectionID, int UserID)
        {
            var collection = await _uowCollec.EntityRepo.FindAsync(CollectionID);
            if (collection == null) throw new ArgumentNullException();


            return collection.CreatedByUserId == UserID || collection.IsPublic;
        }

        public async Task<bool> IsUserQuestionOwnerAsync(HashSet<int> setQuestionIDs, int UserID)
        {
            List<Question> QEntities = await
                _uowQuestion.EntityRepo.GetAllQuestionsAsync(setQuestionIDs);

            foreach (var q in QEntities)
            {
                if (q.UserID != UserID) return false;
            }

            return true;
        }

        public async Task<bool> IsUserChoiceOwnerAsync(int choiceID, int UserID)
        {
            var choice = await _uowChoices.EntityRepo.FindAsync(choiceID);

            if (choice == null) throw new ArgumentNullException();

            return await IsUserQuestionOwnerAsync(choice.QuestionID, UserID);
        }

        public async Task<bool> IsUserSubmitionOwnerAsync(int submitionID, int UserID)
        {
           var subEntity = await _uowCollectionsSubmitions.EntityRepo.FindAsync(submitionID);
           return subEntity != null && subEntity.SubmittedUserID == UserID;
        }

        public async Task<bool> IsUserReviewOwnerAsync(int ReviewID, int UserID)
        {
            var Entity = await _uowCollectionReviews.EntityRepo.FindAsync(ReviewID);
            if (Entity == null) throw new ArgumentNullException();
            return Entity.UserID == UserID;
        }
    }
}
