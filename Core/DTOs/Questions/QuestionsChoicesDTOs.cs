using Core.DTOs.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Questions
{
    public class QuestionsChoicesDTOs
    {

        public class CreateChoiceDTO
        {

            public string ChoiceText { get; set; }

            public bool IsRightAnswer { get; set; }

            public byte Rank { get; set; }


        }


        public class PatchChoiceDTO : CreateChoiceDTO
        {

          

        }


        public class SendChoiceDTO
        {

            public int ChoiceID { get; set; }

            public int QuestionID { get; set; }

            public string ChoiceText { get; set; }

            public byte Rank { get; set; }


        }

        public class SendSimpleChoiceDTO
        {
            public int ChoiceID { get; set; }
            public string ChoiceText { get; set; }
        }

        public class SendExplanationWithRightAnswerDTO
        {
            public int RightAnswerChoiceID { get; set; }
            public string? ExplanationText { get; set; }
            public int? ExplanationID { get; set; }
        }
    }
}
