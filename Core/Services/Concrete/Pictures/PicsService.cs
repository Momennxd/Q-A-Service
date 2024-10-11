using AutoMapper;
using Core.DTOs.Pictures;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Pictures;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Pictures.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Collections.CollectionsDTOs;

namespace Core.Services.Concrete.Pictures
{
    public class PicsService : IPicsService
    {

        private readonly IMapper _mapper;

        private readonly IUnitOfWork<IPicsRepo, Pics> _unitOfWork;



        public PicsService (IMapper mapper, IUnitOfWork<IPicsRepo, Pics> uow)
        {
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<PicsDTOs.SendPicDTOs> CreatePicAsync(PicsDTOs.CreatePicDTOs createPicDTO)
        {

            var entity = _mapper.Map<Pics>(createPicDTO);

            await _unitOfWork.EntityRepo.AddItemAsync(entity);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PicsDTOs.SendPicDTOs>(entity);

        }
    }
}
