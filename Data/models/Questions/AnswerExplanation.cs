using Data.models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Questions
{
    public class AnswerExplanation : IBaseEntity<AnswerExplanation>
    {
        public int ExplanationID { get; set; }
        public string ExplanationText { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int QuestionID { get; set; }
    }

}
