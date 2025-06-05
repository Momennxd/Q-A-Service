using Core.DTOs.Questions;
using Data.models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.chosen_choicesDTOs;

namespace Core.Services.Interfaces
{
    public interface IChosenChoicesService
    {
        public Task<Dictionary<int, send_chosen_choicesDTO>> GetChosenChoices(HashSet<int> QuestionIDs, int submitionID, int userID);

    }
}
