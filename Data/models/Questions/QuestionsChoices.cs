using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Questions
{
    public class QuestionsChoices : IBaseEntity<QuestionsChoices>
    {

        [Key]
        public int ChoiceID { get; set; }


        public int QuestionID { get; set; }

        public string ChoiceText { get; set; }


        public bool IsRightAnswer { get; set; }

        public byte Rank { get; set; }


    }
}
