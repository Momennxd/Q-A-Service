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
        public class MainDTO
        {
            public int InstitutionID { get; set; }
            public int UserID { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string InstitutionName { get; set; }
            public string WebsiteURL { get; set; }
            public DateTime EstablishedYear { get; set; }
        }

        public class SigninDTO
        {
            public UsersDTOs.AddUserDTO userInfo { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string InstitutionName { get; set; }
            public string WebsiteURL { get; set; }
            public DateTime EstablishedYear { get; set; }
        }
    }
}
