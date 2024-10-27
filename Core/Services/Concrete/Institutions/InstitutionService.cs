using AutoMapper;
using Core.DTOs.Institution;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Institutions;
using Data.models.People;
using Data.Repositories;
using Data.Repository.Entities_Repositories;
using Data.Repository.Entities_Repositories.Institutions_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Institution.InstitutionsDTOs;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Concrete.Institutions
{
    public class InstitutionService : IInstitutionServce
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork<IInstitutionsRepo, Institution> _uowInstit;
        private readonly IUnitOfWork<IUserRepo, User> _uowUser;
        private readonly IUnitOfWork<IPersonRepo, Person> _uowPerson;

        public InstitutionService(IMapper mapper, IUnitOfWork<IPersonRepo, Person> uowPerson,
            IUnitOfWork<IInstitutionsRepo, Institution> uowInstit, IUnitOfWork<IUserRepo, User> uowUser)
        {
            _mapper = mapper;
            _uowInstit = uowInstit;
            _uowUser = uowUser;
            _uowPerson = uowPerson;
        }

        public async Task<SendInstitutionDTO?> CreateInstitution(
            CreateInstitutionDTO createInstitutionDTO)
        {

            //add person

            var person = _mapper.Map<Person>(createInstitutionDTO);
            await _uowPerson.EntityRepo.AddItemAsync(person);
            if (await _uowPerson.CompleteAsync() < 1) return null;





            //add user

            var User = _mapper.Map<User>(createInstitutionDTO);
            User.PersonId = person.PersonID;
            await _uowUser.EntityRepo.AddItemAsync(User);
            if (await _uowUser.CompleteAsync() < 1) return null;


            //add institution


            var Instit = _mapper.Map<Institution>(createInstitutionDTO);
            Instit.UserID = User.UserId;
            await _uowInstit.EntityRepo.AddItemAsync(Instit);
            if (await _uowInstit.CompleteAsync() < 1) return null;



            var sendDto = new SendInstitutionDTO();

            _mapper.Map(person, sendDto);
            _mapper.Map(User, sendDto);
            _mapper.Map(Instit, sendDto);


            return sendDto;
        }
    }
}
