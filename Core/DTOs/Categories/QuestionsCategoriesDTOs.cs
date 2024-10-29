using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.nsCategories
{
    public class QuestionsCategoriesDTOs
    {

        public class CreateQuestionsCategoryDTO
        {

            public int CategoryID { get; set; }


        }



        public class SendQuestionsCategoryDTO
        {
            public int Question_CategoryID { get; set; }

            public int QuestionID { get; set; }

            public int CategoryID { get; set; }

            public string CategoryName { get; set; }

        }


        /// <summary>
        /// DTO that is sent to the end user WITHOUT the category name
        /// </summary>
        public class BaseSendQuestionsCategoryDTO
        {
            public int Question_CategoryID { get; set; }

            public int QuestionID { get; set; }

            public int CategoryID { get; set; }


        }







    }
}
