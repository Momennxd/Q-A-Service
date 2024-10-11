using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pictures
{
    public class PicsDTOs
    {

        public class CreatePicDTOs {

            public int PublicID { get; set; }
            public decimal Rank { get; set; }

        }


        public class SendPicDTOs
        {

            public int PicID { get; set; }

            public int PublicID { get; set; }


            public decimal Rank { get; set; }

        }




    }
}
