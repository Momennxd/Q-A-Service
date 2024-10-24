﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pictures
{
    public class ChoicesPicsDTOs
    {

        public class CreateChoicePicDTO
        {
            public PicsDTOs.CreatePicDTO Pic { get; set; }


            public int ChoiceID { get; set; }
        }



        public class SendChoicePicDTO
        {
            public int PicID { get; set; }

            
            public PicsDTOs.SendPicDTO Pic { get; set; }

            public int ChoiceID { get; set; }
        }







    }
}
