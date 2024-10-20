using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization_Services.Interfaces
{
    public interface ICollectionsAuthService
    {


        /// <summary>
        /// checks if the user is the collection creater.
        /// </summary>
        /// <param name="collecID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<bool> IsUserCollecOwnerAsync(int collecID, int UserID);


        public Task<bool> IsUserQuestionOwnerAsync(int QuestionID, int UserID);


        /// <summary>
        /// Determines if the user could retrieve this choice
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<bool> IsUserQuestionAccessAsync(int QuestionID, int UserID);


        /// <summary>
        /// Determines if the user could retrieve this choice
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<bool> IsUserQuestionAccessAsync(HashSet<int> setQuestionIDs, int UserID);



        /// <summary>
        /// Determines if the user could retrieve the right answers of a question.
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<bool> IsRightsAnswersAccessAsync(int QuestionID, int UserID);



      







    }
}
