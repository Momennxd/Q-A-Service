using AutoMapper;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.InstitutionsDTOs;


namespace Core.Profiles
{
    public class InstitutionsProfile : Profile
    {




        public InstitutionsProfile() {

            CreateMap<Institution, SendInstitutionsDTO>();
            CreateMap<CreateInstitutionsDTO, Institution>();

        }












    }
}
