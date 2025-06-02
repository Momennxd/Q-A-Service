using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models._SP_
{
    public class SP_ChoiceWithExplanation
    {
        public int ChoiceID { get; set; }
        public int QuestionID { get; set; }
        public string ChoiceText { get; set; }
        public bool IsRightAnswer { get; set; }
        public string? ExplanationText { get; set; }
        public int? ExplanationID { get; set; }
    }
}
