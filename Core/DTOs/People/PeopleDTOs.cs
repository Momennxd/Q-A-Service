

namespace Core.DTOs.People
{

    public sealed class PeopleDTOs 
    {

        public class BasePersonDTO
        {
            public string FirstName { get; set; }
            public string? SecondName { get; set; }
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public bool? Gender { get; set; }
            public byte CountryID { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string? Email { get; set; }
            public string? Notes { get; set; }
            public short PreferredLanguageID { get; set; }



        }

        public class AddPersonDTO : BasePersonDTO
        {
          

        }
        
        public class UpdateDTO
        {
            public string FirstName { get; set; }
            public string? SecondName { get; set; }
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public string? Notes { get; set; }
            public short PreferredLanguageID { get; set; }
            public byte CountryID { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        public class SendPersonDTO : BasePersonDTO
        {
            public int PersonID { get; set; }

            public DateTime JoinedDate { get; set; }


        }


    }
}
