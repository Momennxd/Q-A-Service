using Core.DTOs.Questions;
using Data.models._SP_;
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


        /// <summary>
        /// This DTO is used to send the  most valuable public info in brief about a collection THROUGH SEARCH BAR.
        /// It's made to be fast without requesting too many time to the DB and with only basic info
        /// </summary>
        public class SendCollectionDTO_Search : CollectionBaseDTO
        {
            public int CollectionID { get; set; }
            public DateTime AddedTime { get; set; }

        }


        /// <summary>
        /// This DTO is used to send the full most valuable info about a collection like questions, choices......
        /// </summary>
        public class SendCollectionDTO_Full : CollectionBaseDTO
        { 
           public int CollectionID { get; set; }
           public DateTime AddedTime { get; set; }

           public long Likes { get; set; }
            public long DisLikes { get; set; }

            public List<QuestionsDTOs.SendQuestionDTO> CollecQuestions { get; set; }


        }

        /// <summary>
        /// This DTO is used to send the  most valuable public info in brief about a collection.
        /// </summary>
        public class SendCollectionDTO_Thumb : CollectionBaseDTO
        {
            public int CollectionID { get; set; }
            public DateTime AddedTime { get; set; }

            public List<SPCollectionCetagory> Categories { get; set; }

        }

        public class CreateQCollectionDTO : CollectionBaseDTO
        {
            public List<QuestionsDTOs.CreateQuestionDTO> CollecQuestions { get; set; }

        }

        public class PatchQCollectionDTO : CollectionBaseDTO
        {

        }


    }
}
