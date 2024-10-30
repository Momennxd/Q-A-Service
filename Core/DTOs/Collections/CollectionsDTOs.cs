using Core.DTOs.Questions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Core.DTOs.Collections
{

    public static class CollectionsDTOs
    {


        public class CollectionBaseDTO
        {
            public string CollectionName { get; set; }
            public string? Description { get; set; }
            public bool IsPublic { get; set; }
        }

        

        public class SendCollectionDTO : CollectionBaseDTO
        { 
           public int CollectionID { get; set; }
           public DateTime AddedTime { get; set; }

        }


        public class CreateQCollectionDTO : CollectionBaseDTO
        {
            public List<QuestionsDTOs.CreateQuestionDTO> Questions { get; set; }

        }




    }
}
