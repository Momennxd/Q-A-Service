using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Questions
{
    public class Questions : IBaseEntity<Questions>
    {
        [Key]
        public int QuestionID { get; set; }

        public string QuestionText { get; set; }

        public int UserID { get; set; }

        public bool IsMCQ { get; set; }

        public DateTime AddedDate { get; set; }

        public int ExplanationID { get; set; }

        public bool IsDeleted { get; set; }



    }
}
