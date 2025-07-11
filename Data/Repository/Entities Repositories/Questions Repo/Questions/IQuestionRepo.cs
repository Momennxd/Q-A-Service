using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Questions;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo
{
    public interface IQuestionRepo : IRepository<Question>
    {

        public Task<Question?> GetQuestionAsync(int QuestionID);
        public Task<bool> IsUserRightAnswerAccessAsync(int QuestionID, int UserID);

        public Task<List<SP_Question>> GetAllQuestionsAsync(int CollectionID);

        public Task<int> PatchQuestionPointsAsync(int QuestionID, int NewPointsVal);

        public Task<List<Question>> GetAllQuestionsAsync(HashSet<int> QuestionIDs);

        public Task<List<Question>> GetRandomQuestionsWithChoicesAsync(int CollectionID);


        /// <summary>
        /// Deletes a question base on the question ID by a stored prosedure that deletes all the tables that refrence
        /// the table primary key in a single trasaction.
        /// </summary>
        /// <param name="QuestionIDs"></param>
        /// <returns></returns>
        public Task<int> DeleteQuestionAsync(int QuestionIDs);


        public Task<SP_RightChoiceWithExplanation?> GetRightChoiceWithExplanationAsync(int questionId);



    }
}
