using Core.DTOs.Institution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IInstitutionServce
    {
        Task<int> Siginin(InstitutionsDTOs.SigninDTO sigininDTO);
    }
}
