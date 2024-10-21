using Data.models._SP_;
using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo
{
    public interface IQuestionRepo : IRepository<Question>
    {


        public Task<bool> IsUserRightAnswerAccessAsync(int QuestionID, int UserID);

        public Task<List<SP_Question>> GetAllQuestionsAsync(int CollectionID);



    }
}
