using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.QuestionsDTOs;

namespace Core.Services.Interfaces
{
    public interface IQuestionsService
    {


        public Task<List<SendQuestionDTO>> CreateQuestionsAsync(
            List<CreateQuestionDTO> createQuestionsDTO, int CollectionID, int UserID);



        public Task<List<SendQuestionDTO>> GetAllQuestionsAsync(int CollectionID);


        public Task<SendQuestionDTO> PatchQuestionAsync(
           JsonPatchDocument<PatchQuestionDTO> patchDoc, int collecID);



        public Task<int> PatchQuestionPointsAsync(int QuestionID, int NewPointsVal);







    }
}
