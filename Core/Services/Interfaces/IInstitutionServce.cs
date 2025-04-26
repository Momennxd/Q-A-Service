using Core.DTOs.Institution;
using Data.models.Institutions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Institution.InstitutionsDTOs;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Services.Interfaces
{
    public interface IInstitutionServce
    {
        Task<SendInstitutionDTO?> CreateInstitution(CreateInstitutionDTO createInstitutionDTO);

        public Task<SendInstitutionDTO?> GetInstitutionAsync(dynamic id);

        Task<SendInstitutionDTO> PatchInst(JsonPatchDocument<PatchInstitutionDTO> patchDoc, int instID);


        public Task<Institution?> GetInstitutionByUserIDAsync(int UserID);

    }
}
