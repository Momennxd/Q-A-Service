﻿using Core.DTOs.Questions;
using Data.models._SP_;
using Data.models.Questions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;
using static Core.DTOs.Questions.QuestionsDTOs;

namespace Core.Services.Interfaces
{
    public interface IQuestionsService
    {


        public Task<List<SendQuestionDTO>> CreateQuestionsAsync(
            List<CreateQuestionDTO> createQuestionsDTO, int CollectionID, int UserID);



        public Task<List<SendQuestionDTO>> GetAllQuestionsAsync(int CollectionID);

        public Task<SendQuestionDTO> GetQuestionAsync(int QuestionID);

        public Task<SendQuestionDTO> PatchQuestionAsync(
           JsonPatchDocument<PatchQuestionDTO> patchDoc, int collecID);



        public Task<int> PatchQuestionPointsAsync(int QuestionID, int NewPointsVal);


        public Task<int> DeleteQuestionAsync(int QuestionID);


        public Task<List<QuestionsDTOs.QuestionWithChoicesDto>> GetRandomQuestionsWithChoicesAsync(int CollectionID);


        public Task<SendExplanationWithRightAnswerDTO> GetRightAnswerWithExplnanation(int questionID);

    }
}
