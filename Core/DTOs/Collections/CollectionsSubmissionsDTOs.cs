using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Collections
{
    public class CollectionsSubmissionsDTOs
    {

        /// <summary>
        /// A simple DTO for collection submission simple data.
        /// </summary>
        public class SendCollectionSubmissionThumbDTO
        {
            public int SubmitionID { get; set; }

            public int SubmittedUserID { get; set; }

            public DateTime SubmitDate { get; set; }

            public int CollectionID { get; set; }

        }


      
       
    }
}
