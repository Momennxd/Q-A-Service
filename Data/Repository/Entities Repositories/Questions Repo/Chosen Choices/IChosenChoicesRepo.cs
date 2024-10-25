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




    }
}
