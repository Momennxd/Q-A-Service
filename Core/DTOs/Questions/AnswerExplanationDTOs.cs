using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Questions
{
    public class AnswerExplanationDTOs
    {

        public class AnswerExplanationMainDTO
        {
            public string ExplanationText { get; set; }
            public int QuestionID { get; set; }

        }


        public class GetAnswerExplanationDTO : AnswerExplanationMainDTO
        {
            public int ExplanationID { get; set; }

            public DateTime AddedDate { get; set; }
        }



    }
}
