using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models._SP_
{
    public class SP_RightChoiceWithExplanation
    {
        public int ChoiceID { get; set; }
        public string? ExplanationText { get; set; }
        public int? ExplanationID { get; set; }
    }
}
