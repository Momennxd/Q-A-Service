using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Collections
{
    public class CollectionsSubmissionsDTOs
    {

        public class MainDTO
        {
            public int CollectionID { get; set; }
        }
        public class CollectionSubmissionMainDTO
        {
            public DateTime SubmitDate { get; set; }
            public string Username { get; set; }
            public int TotalChosenChoices { get; set; }
            public int TotalRightAnswers { get; set; }
        }
    }
}
