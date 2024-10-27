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
            public int UserID { get; set; }

            public string Username { get; set; }
            public string Password { get; set; }

            public PeopleDTOs.SendPersonDTO Person { get; set; }

        }


        public record LoginDTO
            (
                string Username,
                string Password
            );



    }
}
