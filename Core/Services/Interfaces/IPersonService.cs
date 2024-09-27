using Core.DTOs.People;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IPersonService
    {

        Task<PeopleDTOs.SendPersonDTO> GetPersonByIdAsync(int id);
        Task<IEnumerable<PeopleDTOs.SendPersonDTO>> GetAllPersonsAsync();
        Task<int> CreatePersonAsync(PeopleDTOs.AddPersonDTO Person);
        Task<int> UpdatePersonAsync(int id, PeopleDTOs.AddPersonDTO Person);
        Task<int> DeletePersonAsync(int id);



    }
}
