using DataAccess_Layer.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities.People
{
    public class ePersonDA : Repository<ePersonDA>
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public bool? Gender { get; set; }

        [Required]
        public byte CountryID { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public short PreferredLanguageID { get; set; }

        public string PassportNumber { get; set; }

        public string NationalNumber { get; set; }

        public string ProfilePicURL { get; set; }


    }
}
