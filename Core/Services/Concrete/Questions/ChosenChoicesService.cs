using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Questions
{
    public class ChosenChoicesService : IChosenChoicesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IChosenChoicesRepo, Chosen_Choices> _uowChosenChoices;

        public async Task<Dictionary<int, chosen_choicesDTOs.send_chosen_choicesDTO>> GetChosenChoices(HashSet<int> QuestionIDs, int submitionID, int userID)
        {
            var res = await _uowChosenChoices.EntityRepo.GetChosenChoices(QuestionIDs, submitionID, userID);

            Dictionary<int, chosen_choicesDTOs.send_chosen_choicesDTO> ans = res.ToDictionary(
                pair => pair.Key,
                pair => new chosen_choicesDTOs.send_chosen_choicesDTO
                {
                    Chosen_ChoiceID = pair.Value.Chosen_ChoiceID,
                    ChoiceID = pair.Value.ChoiceID,
                    UserID = pair.Value.UserID,
                    ChosenDate = pair.Value.ChosenDate,
                    SubmitionID = pair.Value.SubmitionID
                });

            return ans;
        }

    }


}
