using AutoMapper;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.People
{
    public class PeopleService 
    {



        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        //public PeopleService(ILogger<IUnitOfWork> logger, AppDbContext context, IMapper mapper)
        //{
        //    _unitOfWork = new UnitOfWork(logger, context);
        //    _mapper = mapper;
        //}

        //public async Task<int> CreatePersonAsync(PeopleDTOs.AddPersonDTO person)
        //{
        //     await _unitOfWork.People.AddItemAsync(_mapper.Map<Person>(person));
        //    return await _unitOfWork.CompleteAsync();
        //}

        //public async Task<int> DeletePersonAsync(int id)
        //{
        //    await _unitOfWork.People.DeleteItemAsync(id);
        //    return await _unitOfWork.CompleteAsync();

        //}

        //public async Task<IEnumerable<PeopleDTOs.SendPersonDTO>> GetAllPersonsAsync()
        //{
        //    // Retrieve all people and map the collection to a collection of SendPersonDTO
        //    var people = await _unitOfWork.People.GetAllItemsAsync();

         

        //    return _mapper.Map<IEnumerable<PeopleDTOs.SendPersonDTO>>(people);
        //}


        //public async Task<PeopleDTOs.SendPersonDTO> GetPersonByIdAsync(int id)
        //{
        //    var person = await _unitOfWork.People.FindAsync(id);

        //    // Check if person exists before mapping
        //    if (person == null)
        //    {
        //        return null;
        //    }

        //    // Map the person entity to SendPersonDTO and return
        //    return _mapper.Map<PeopleDTOs.SendPersonDTO>(person);
        //}


        //public async Task<int> UpdatePersonAsync(int id, PeopleDTOs.AddPersonDTO person)
        //{
        //    // Find the person first
        //    var existingPerson = await _unitOfWork.People.FindAsync(id);

        //    if (existingPerson == null)
        //    {
        //        // Optionally, handle the case where the person does not exist
        //        throw new KeyNotFoundException($"Person with Id {id} not found.");
        //    }

        //    // Map the incoming DTO to the existing entity (this will update only the fields you want)
        //    _mapper.Map(person, existingPerson);

        //    // Update the item in the repository
        //    await _unitOfWork.People.UpdateItemAsync(id, existingPerson);

        //    // Commit the changes and return the result
        //    return await _unitOfWork.CompleteAsync();
        //}

    }
}
