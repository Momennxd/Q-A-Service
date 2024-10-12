using AutoMapper;
using Core.DTOs.Pictures;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Pictures;
using Data.Repository.Entities_Repositories.Pics.Choices_Pics_Repo;
using Data.Repository.Entities_Repositories.Pictures.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Pictures
{
    public class ChoicesPicsService : IChoicesPicsService
    {



        private readonly IMapper _mapper;

        private readonly IUnitOfWork<IChoicesPicsRepo, Choices_Pics> Choices_unitOfWork;
        private readonly IPicsService _picsService;


        public ChoicesPicsService(IMapper mapper, IUnitOfWork<IChoicesPicsRepo, Choices_Pics> uow,
            IPicsService picsService)
        {
            Choices_unitOfWork = uow;
            _picsService = picsService;
            _mapper = mapper;
        }

        public async Task<ChoicesPicsDTOs.SendChoicePicDTO?> CreateChoicePicAsync
            (ChoicesPicsDTOs.CreateChoicePicDTO createChoicePicDTO)
        {

            var sendPicDto = await _picsService.CreatePicAsync(createChoicePicDTO.Pic);

            var choicePicEntity = _mapper.Map<Choices_Pics>(createChoicePicDTO);

            choicePicEntity.PicID = sendPicDto.PicID;

            await Choices_unitOfWork.EntityRepo.AddItemAsync(choicePicEntity);

            await Choices_unitOfWork.CompleteAsync();

            var outputDTO = _mapper.Map<ChoicesPicsDTOs.SendChoicePicDTO>(choicePicEntity);

            outputDTO.Pic = sendPicDto;

            return outputDTO;
        }
    }
}
