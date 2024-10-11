using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pictures
{
    public class ChoicesPicsDTOs
    {


        public class CreateChoicePicDTO
        {

            public PicsDTOs.CreatePicDTOs Pic { get; set; }


            public int ChoiceID { get; set; }
        }



        public class SendChoicePicDTO
        {
            public int PicID { get; set; }

            public PicsDTOs.SendPicDTOs Pic { get; set; }


            public int ChoiceID { get; set; }
        }







    }
}
