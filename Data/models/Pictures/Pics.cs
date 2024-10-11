using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Pictures
{
    public class Pics : IBaseEntity<Pics>
    {


        [Key]
        public int PicID { get; set; }


        public int PublicID { get; set; }


        public decimal Rank { get; set; }

    }

}
