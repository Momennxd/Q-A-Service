using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.People
{
    public class UsersDTOs
    {
        public record AddUserDTO(
                string Username,
                string Password,
                PeopleDTOs.AddPersonDTO Person,
                bool IsActive,
                bool IsDeleted
            );


        public record LoginDTO
            (
                string Username,
                string Password
            );



       
    }
}
