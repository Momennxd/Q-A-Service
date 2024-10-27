using AutoMapper;
using Core.DTOs.Institution;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Institutions;
using Data.models.People;
using Data.Repositories;
using Data.Repository.Entities_Repositories;
using Data.Repository.Entities_Repositories.Institutions_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Concrete.Institutions
{
    public class InstitutionService : IInstitutionServce
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IInstitutionsRepo, Institution> _unitOfWork;

        public InstitutionService(IMapper mapper, IUnitOfWork<IInstitutionsRepo, Institution> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Siginin(InstitutionsDTOs.SigninDTO sigininDTO)
        {
            var institution = _mapper.Map<Institution>(sigininDTO);
            await _unitOfWork.EntityRepo.AddItemAsync(institution);
            await _unitOfWork.CompleteAsync();

            return institution.UserID;
        }
    }
}
