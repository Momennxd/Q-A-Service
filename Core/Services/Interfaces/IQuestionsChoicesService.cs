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



        public Task<List<QuestionsChoicesDTOs.SendChoiceDTO>> AddChoiceAsync
             (List<QuestionsChoicesDTOs.CreateChoiceDTO> lstcreateChoiceDto);







    }
}
