namespace Core.DTOs.People
{
    public class UsersDTOs
    {
        public class AddUserDTO
        {

            public string Username;
            public string Password;
            public PeopleDTOs.AddPersonDTO Person;
            public bool IsActive;
            public bool IsDeleted;

        }


        public record LoginDTO
            (
                string Username,
                string Password
            );

        public record UpdateDTO
            (
                string Username,
                string Password,
                PeopleDTOs.UpdateDTO PersonInfo
            );


    }
}
