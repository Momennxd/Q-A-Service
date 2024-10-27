using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.People
{
    public static class InstitutionsDTOs
    {

        public class baseInstDTO
        {
            public byte CountryId { get; set; }

            public string? Email { get; set; }

            public string? Notes { get; set; }

            public short PreferredLanguageId { get; set; }

            public string Username { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string? Latitute { get; set; }

            public string? Longitute { get; set; }

            public string? WebsiteURL { get; set; }

            public DateOnly EstablishedYear { get; set; }
        }


        public class CreateInstitutionsDTO : baseInstDTO
        {
          


        }



        public class SendInstitutionsDTO : baseInstDTO
        {

            public int InstitutionID { get; set; }

            


        }



    }

}
