using Core_Layer.Entities.People;

namespace Core_Layer.DTOs.People
{

    public class PeopleDTOs 
    {
        public record AddPersonDTO(
            string FirstName,
            string? SecondName,
            string? LastName,
            string? Address,
            bool? Gender,
            byte CountryID,
            DateTime DateOfBirth,
            string? Email,
            string? Notes,
            short PreferredLanguageID,
            string? ProfilePicURL
        );

        public static Entities.People.ePersonDA? ConvertFromDTOtoEntity(AddPersonDTO dto)
        {
            if(dto == null) return null;



            return new ePersonDA() 
            { 
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                LastName = dto.LastName,
                Address = dto.Address, 
                Gender = dto.Gender,
                CountryID = dto.CountryID,
                DateOfBirth = dto.DateOfBirth,
                JoinedDate = DateTime.Now,
                Email = dto.Email,
                Notes = dto.Notes,
                PreferredLanguageID = dto.PreferredLanguageID,
                ProfilePicURL = dto.ProfilePicURL
            };
        }

    }
}
