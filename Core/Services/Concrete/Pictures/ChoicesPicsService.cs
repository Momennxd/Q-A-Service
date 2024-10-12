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
        private readonly IUnitOfWork<IPicsRepo, Pics> Pics_unitOfWork;



        public ChoicesPicsService(IMapper mapper, IUnitOfWork<IChoicesPicsRepo, Choices_Pics> uow,
            IUnitOfWork<IPicsRepo, Pics> PicsUnitOfWork)
        {
            Choices_unitOfWork = uow;
            Pics_unitOfWork = PicsUnitOfWork;
            _mapper = mapper;
        }

        public async Task<ChoicesPicsDTOs.SendChoicePicDTO?> CreateChoicePicAsync
            (ChoicesPicsDTOs.CreateChoicePicDTO createChoicePicDTO)
        {

            var picEntity = _mapper.Map<Pics>(createChoicePicDTO.Pic);
            await Pics_unitOfWork.EntityRepo.AddItemAsync(picEntity);

            if (await Pics_unitOfWork.CompleteAsync() != 1)
                return null;
                    


            var choicePicEntity = _mapper.Map<Choices_Pics>(createChoicePicDTO);
            choicePicEntity.PicID = picEntity.PicID;
            await Choices_unitOfWork.EntityRepo.AddItemAsync(choicePicEntity);
            await Choices_unitOfWork.CompleteAsync();


            var outputDTO = _mapper.Map<ChoicesPicsDTOs.SendChoicePicDTO>(choicePicEntity);
            outputDTO.Pic = _mapper.Map<PicsDTOs.SendPicDTOs>(picEntity);

            return outputDTO;
        }
    }
}
