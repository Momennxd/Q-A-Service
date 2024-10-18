using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging.Abstractions;
using Core.DTOs.Pictures;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace Core.Services.Concrete.Questions
{
    public class QuestionsChoicesService : IQuestionsChoicesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _unitOfWork;


        public QuestionsChoicesService
            (IMapper mapper, IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowChoices)
        {
            _unitOfWork = uowChoices;
            _mapper = mapper;
        }



        public async Task<List<SendChoiceDTO>> AddChoiceAsync 
            (List<CreateChoiceDTO>  lstcreateChoiceDto)
        {
            List<QuestionsChoices> CreateChoicesEnities = new List<QuestionsChoices>();

            foreach (var choice in lstcreateChoiceDto) {
                CreateChoicesEnities.Add(_mapper.Map<QuestionsChoices>(choice));
            }

            //entity.ChoiceText = createChoiceDto.ChoiceText;

            await _unitOfWork.EntityRepo.AddItemsAsync(CreateChoicesEnities);

            await _unitOfWork.CompleteAsync();


            List<SendChoiceDTO> QSendchoicesDTOs = new List<SendChoiceDTO>();

            foreach (var e in CreateChoicesEnities) {
                QSendchoicesDTOs.Add(_mapper.Map<SendChoiceDTO>(e));
            }

            return QSendchoicesDTOs;



        }


    }
}
