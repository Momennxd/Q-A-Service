using Core_Layer.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs.People
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
        public static eUsersDA? ConvertFromDTOtoEntity(AddUserDTO dto)
        {
            if (dto == null) return null;


            return new eUsersDA()
            {
                Username = dto.Username,
                Password = dto.Password,
                IsDeleted = dto.IsDeleted,
                IsACtive = dto.IsActive,
            };
        }
    }
}
