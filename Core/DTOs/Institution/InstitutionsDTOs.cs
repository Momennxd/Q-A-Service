using Core.DTOs.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Institution
{
    public class InstitutionsDTOs
    {
      

        public class InstitutionDTO
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string InstitutionName { get; set; }
            public string WebsiteURL { get; set; }
            public DateTime EstablishedYear { get; set; }



            public string Username { get; set; }
            public string Password { get; set; }



            public string? Notes { get; set; }
            public short PreferredLanguageID { get; set; }
            public byte CountryID { get; set; }
        }


        public class CreateInstitutionDTO : InstitutionDTO
        {
            
        }


        public class SendInstitutionDTO
        {
            public int InstitutionID { get; set; }

            public string latitude { get; set; }
            public string longitude { get; set; }
            public string InstitutionName { get; set; }
            public string WebsiteURL { get; set; }
            public DateTime EstablishedYear { get; set; }

            public DTOs.People.UsersDTOs.SendUserDTO sendUser;
        }
    }
}
