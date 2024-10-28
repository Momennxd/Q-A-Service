using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.nsCategories
{
    public class Categories : IBaseEntity<Categories>
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }


    }

}
