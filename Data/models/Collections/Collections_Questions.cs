using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class Collections_Questions : IBaseEntity<Collections_Questions>
    {
        [Key]
        public int Collection_QuestionID { get; set; }

        public int CollectionID { get; set; }

        public int QuestionID { get; set; }

        public DateTime AddedTime { get; set; }

        public int QuestionPoints { get; set; }

    }
}
