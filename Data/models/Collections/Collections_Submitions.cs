using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class Collections_Submitions
    {
        [Key]
        public int SubmitionID { get; set; }

        public int SubmittedUserID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime SubmitDate { get; set; }

        public int CollectionID { get; set; }
    }
}
