

namespace Core.DTOs.People
{

    public class PeopleDTOs 
    {
        public class AddPersonDTO
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
            public string? ProfilePicURL { get; set; }




        }





        public class AddUserDTO
        {
            AddPersonDTO person;

            public string username;
        }




        //public static Entities.People.ePersonDA? ConvertFromDTOtoEntity(AddPersonDTO dto)
        //{
        //    if(dto == null) return null;



        //    return new ePersonDA() 
        //    { 
        //        FirstName = dto.FirstName,
        //        SecondName = dto.SecondName,
        //        LastName = dto.LastName,
        //        Address = dto.Address, 
        //        Gender = dto.Gender,
        //        CountryID = dto.CountryID,
        //        DateOfBirth = dto.DateOfBirth,
        //        JoinedDate = DateTime.Now,
        //        Email = dto.Email,
        //        Notes = dto.Notes,
        //        PreferredLanguageID = dto.PreferredLanguageID,
        //        ProfilePicURL = dto.ProfilePicURL
        //    };
        //}

    }
}
