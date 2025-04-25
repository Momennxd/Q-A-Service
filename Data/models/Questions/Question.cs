using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Questions
{
    public class Question : IBaseEntity<Question>
    {
        [Key]
        public int QuestionID { get; set; }

        public string QuestionText { get; set; }

        public int UserID { get; set; }

        public bool IsMCQ { get; set; }

        public DateTime AddedDate { get; set; }

        public byte Rank { get; set; }

        public virtual List<QuestionsChoices> Choices { get; set; }



    }
}
