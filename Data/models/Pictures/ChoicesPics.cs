﻿using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Pictures
{
    public class ChoicesPics : IBaseEntity<ChoicesPics>
    {

        [Key]        
        public int Choice_PicID { get; set; }


        public int PicID { get; set; }



        public int ChoiceID { get; set; }
    }
}
