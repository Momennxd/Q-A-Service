using Core.DTOs.Institution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Institution.InstitutionsDTOs;

namespace Core.Services.Interfaces
{
    public interface IInstitutionServce
    {
        Task<SendInstitutionDTO?> CreateInstitution(InstitutionsDTOs.CreateInstitutionDTO createInstitutionDTO);
    }
}
