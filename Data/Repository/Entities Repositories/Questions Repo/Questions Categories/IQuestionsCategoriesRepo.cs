using Data.models._SP_;
using Data.models.nsCategories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.nsQuestions_Categories
{
    public interface IQuestionsCategoriesRepo : IRepository<Questions_Categories>
    {

        public Task<List<SP_QuestionCategories>> GetQuestionCategoriesAsync(int QuestionID);


        /// <summary>
        /// Deletes all the mapping between categories and questions based on question id
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns>
        /// Number of affected rows.
        /// </returns>
        public Task<int> DeleteQuestionCategoriesAsync(int QuestionID);




    }
}
