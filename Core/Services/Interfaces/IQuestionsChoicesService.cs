using Core.DTOs.Collections;
using Core.DTOs.Questions;
using Data.models.Questions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace Core.Services.Interfaces
{
    public interface IQuestionsChoicesService
    {



        public Task<List<SendChoiceDTO>> AddChoiceAsync
             (List<CreateChoiceDTO> lstcreateChoiceDto, int QuestionID);

    

        public Task<List<SendChoiceDTO>> GetChoicesAsync(int QuestionID);

        public Task<Dictionary<int, List<SendChoiceDTO>>> GetChoicesAsync(HashSet<int> QuestionsIDs);


        public Task<List<SendChoiceDTO>> GetAllRightAnswersAsync(int Questionid);

        public Task<bool> IsRightAnswerAsync(int choiceid);

        public Task<int> DeleteChoiceAsync(int choiceid);


        public Task<int> DeleteQuestionChoicesAsync(int QuestionID);



        public Task<SendChoiceDTO> PatchChoiceAsync(JsonPatchDocument<PatchChoiceDTO> patchDoc, int ChoiceID);


        public Task<SendChoiceWithExplanationDTO?> GetChoiceWithExplanationAsync(int choiceId, int questionId);


    }
}
