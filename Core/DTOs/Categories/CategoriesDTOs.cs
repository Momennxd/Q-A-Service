using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Categories
{
    public  class CategoriesDTOs
    {



        public class CreateCategoryDTO
        {
            public string CategoryName { get; set; }

        }



        public class SendCategoryDTO
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }

        }






    }
}
