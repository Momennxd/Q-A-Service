using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models._SP_
{

    /// <summary>
    /// this dbset maps to SP_GetCollectionQuestionsDTO, It retrieves all the questions for a collection
    /// </summary>
    public class SP_Question
    {


        public int QuestionID { get; set; }

        public string QuestionText { get; set; }

        public int UserID { get; set; }

        public bool IsMCQ { get; set; }

        public DateTime AddedDate { get; set; }

        public int QuestionPoints { get; set; }

        public byte Rank { get; set; }

        public int CollectionID { get; set; }






    }
}
