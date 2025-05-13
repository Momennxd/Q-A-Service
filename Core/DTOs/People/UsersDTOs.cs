namespace Core.DTOs.People
{
    public class UsersDTOs
    {
        public class AddUserDTO
        {

            public string Username { get; set; }
            public string Password { get; set; }
            public PeopleDTOs.AddPersonDTO Person { get; set; }

        }

        public class SendUserDTO
        {
            public int UserId { get; set; }

            public string Username { get; set; }
            public PeopleDTOs.SendPersonDTO Person { get; set; }

        }

        public class GetUserDTO
        {
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string LastName { get; set; }
            public bool Gender { get; set; }
            public string Email { get; set; }
            public string Notes { get; set; }
            public int UserPoints { get; set; }
        }

        public record LoginDTO
            (
                string Username,
                string Password
            );



    }
}
