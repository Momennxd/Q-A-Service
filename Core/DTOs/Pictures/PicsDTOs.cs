using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Pictures
{
    public class PicsDTOs
    {
        public class PicDTO
        {

            public FormFile file { get; set; }
            public string FolderPath { get; set; }
            public string FileName { get; set; }

        }
        public class CreatePicDTO {


            public PicDTO pic { get; set; } = new PicDTO();
            public decimal Rank { get; set; }

        }


        public class SendPicDTO
        {

            public int PicID { get; set; }

            public string Url { get; set; }


            public decimal Rank { get; set; }

        }




    }
}
