using AutoMapper;
using Core.DTOs.Institution;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Institutions;
using Data.models.People;
using Data.Repositories;
using Data.Repository.Entities_Repositories;
using Data.Repository.Entities_Repositories.Institutions_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
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
            
            
            //add user
            var User = _mapper.Map<User>(createInstitutionDTO);
            User.Person = person;
            
            
            //add institution
            var Instit = _mapper.Map<Institution>(createInstitutionDTO);
            Instit.User = User;
            await _uowInstit.EntityRepo.AddItemAsync(Instit);
            if (await _uowInstit.CompleteAsync() < 1) return null;



            var sendDto = new SendInstitutionDTO();

            _mapper.Map(person, sendDto);
            _mapper.Map(User, sendDto);
            _mapper.Map(Instit, sendDto);


            return sendDto;
        }

        public async Task<SendInstitutionDTO?> GetInstitutionAsync(dynamic id)
        {
            SendInstitutionDTO res;

            Institution inst = await _uowInstit.EntityRepo.GetInstitutionAsync(id);

            res = _mapper.Map<SendInstitutionDTO>(inst);
            res.sendUser = _mapper.Map<SendUserDTO>(inst.User);

            return res;
        }

        public async Task<Institution?> GetInstitutionByUserIDAsync(int UserID)
        {
            if (UserID < 1) return null;
            return await _uowInstit.EntityRepo.GetInstitutionByUserIDAsync(UserID);
        }

        public async Task<SendInstitutionDTO> PatchInst(JsonPatchDocument<PatchInstitutionDTO> patchDoc, int instID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _uowInstit.EntityRepo.GetInstitutionAsync(instID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"Institution with ID {instID} not found.");
            }



            // Map the entity to a DTO to apply the patch
            var InstitutionDTOToPatch = _mapper.Map<PatchInstitutionDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(InstitutionDTOToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(InstitutionDTOToPatch, entity);

            // Save changes
            await _uowInstit.CompleteAsync();

            //getting the send user dto
            var sendDto = _mapper.Map<SendInstitutionDTO>(entity);

            //getting the person send dto 
            sendDto.sendUser = _mapper.Map<UsersDTOs.SendUserDTO>(entity.User);
            sendDto.sendUser.Person = _mapper.Map<PeopleDTOs.SendPersonDTO>(entity.User.Person);


            return sendDto;
        }


    }
}
