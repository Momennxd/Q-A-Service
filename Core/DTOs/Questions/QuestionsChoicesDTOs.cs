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

            public int QuestionID { get; set; }

            public string ChoiceText { get; set; }

            public bool IsRightAnswer { get; set; }

        }


        public class SendChoiceDTO
        {

            public int ChoiceID { get; set; }

            public int QuestionID { get; set; }

            public string ChoiceText { get; set; }


        }




    }
}
