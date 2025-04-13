using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.models.Base;

namespace Data.models.Questions
{
    public class SP_GetRandomQuestion : IBaseEntity<QuestionsChoices>
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public byte QuestionPoints { get; set; }

        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
        public bool IsRightAnswer { get; set; }
        public byte ChoicePoints { get; set; }
    }
}
