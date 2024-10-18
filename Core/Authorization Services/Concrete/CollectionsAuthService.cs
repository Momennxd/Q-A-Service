using AutoMapper;
using Core.Authorization_Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions;
using Data.Repository.Entities_Repositories.Questions_Repo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization_Services.Concrete
{
    public class CollectionsAuthService : ICollectionsAuthService
    {

        private readonly IUnitOfWork<ICollectionRepo, QCollection> _uowCollec;
        private readonly IUnitOfWork<IQuestionRepo, Questions> _uowQuestion;

        public CollectionsAuthService(IUnitOfWork<ICollectionRepo, QCollection> uowCollections,
            IUnitOfWork<IQuestionRepo, Questions> uowQuestion)
        {
            _uowCollec = uowCollections;
            _uowQuestion = uowQuestion;
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
    }
}
