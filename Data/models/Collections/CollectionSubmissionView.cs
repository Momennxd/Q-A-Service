using Data.models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class CollectionSubmissionView : IBaseEntity<CollectionSubmissionView>
    {
        public int SubmitionID { get; set; }
        public DateTime SubmitDate { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int TotalChosenChoices { get; set; }
        public int TotalRightAnswers { get; set; }
    }

}
