using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Questions
{
    public static class QuestionsDTOs
    {    
    

        public class CreateQuestionDTO
        {

            public string QuestionText { get; set; }


            public bool IsMCQ { get; set; }

            public int QuestionPoints { get; set; }

            public byte Rank { get; set; }



            public List<QuestionsChoicesDTOs.CreateChoiceDTO> Choices { get; set; }



        }


        public class SendQuestionDTO
        {

            public int QuestionID { get; set; }

            public string QuestionText { get; set; }
            public int UserID { get; set; }

            public bool IsMCQ { get; set; }

            public DateTime AddedDate { get; set; }

            public int QuestionPoints { get; set; }

            public byte Rank { get; set; }


            public List<QuestionsChoicesDTOs.SendChoiceDTO> Choices { get; set; }


        }



    }
}
