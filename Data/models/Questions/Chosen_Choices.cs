using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Questions
{
    public class Chosen_Choices : IBaseEntity<Chosen_Choices>
    {
        [Key]
        public int Chosen_ChoiceID { get; set; }

        public int ChoiceID { get; set; }

        public int UserID { get; set; }

        public DateTime ChosenDate { get; set; }


        public int SubmitionID { get; set; }

    }
}
