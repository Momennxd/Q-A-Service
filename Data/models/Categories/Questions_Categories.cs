using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.nsCategories
{
    public class Questions_Categories : IBaseEntity<Questions_Categories>
    {
        [Key]
        public int Question_CategoryID { get; set; }


        public int QuestionID { get; set; }

        public int CategoryID { get; set; }


    }
}
