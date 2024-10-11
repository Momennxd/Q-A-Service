using Core.DTOs.Questions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IQuestionsChoicesService
    {

        
        public Task<QuestionsChoicesDTOs.SendChoiceDTO> FindAsync(int choice);


        public Task<List<QuestionsChoicesDTOs.SendChoiceDTO>> GetAllQuestionChoiceAsync(int QuestionID);


        public Task<QuestionsChoicesDTOs.SendChoiceDTO> AddChoiceAsync
            (QuestionsChoicesDTOs.CreateChoiceDTO createChoiceDto);


        public Task<QuestionsChoicesDTOs.SendChoiceDTO> UpdateChoiceAsync
           (QuestionsChoicesDTOs.CreateChoiceDTO updateChoiceDto, int choiceID);


        public Task<bool> DeleteChoiceAsync(int choiceID);





    }
}
