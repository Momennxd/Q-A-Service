using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.chosen_choicesDTOs;

namespace Core.DTOs.Questions
{
    public class chosen_choicesDTOs
    {
        public class chosen_choicesDTO
        { 
            public int Chosen_ChoiceID { get; set; }

            public int ChoiceID { get; set; }

            public int UserID { get; set; }

            public DateTime ChosenDate { get; set; }

            public int SubmitionID { get; set; }
        }

        public class send_chosen_choicesDTO : chosen_choicesDTO {
           
        }


        public class Add_chosen_choicesDTO
        {

            public int ChoiceID { get; set; }

            public int SubmitionID { get; set; }
        }
    }
}
