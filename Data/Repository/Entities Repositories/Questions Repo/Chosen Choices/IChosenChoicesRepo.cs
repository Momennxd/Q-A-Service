using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices
{
    public interface IChosenChoicesRepo : IRepository<Chosen_Choices>
    {



        public Task<int> DeleteChosenChoicesAsync(int ChoiceID);

        public Task<int> DeleteChosenChoicesAsync(HashSet<int> ChoiceIDs);


        /// <summary>
        /// Gets a choice that a user chose for a question inside a specifec submition
        /// (each question has ONLY 1 chosen choice in one submtion for a user)
        /// </summary>
        /// <param name="QuestionIDs"></param>
        /// <param name="submitionID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Task<Dictionary<int, Chosen_Choices>> GetChosenChoices(HashSet<int> QuestionIDs, int submitionID, int userID);



    }
}
