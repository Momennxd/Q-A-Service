using Data.models.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class CollectionsCategories : IBaseEntity<CollectionsCategories>
    {

        [Key]
        public int CollectionCategoryID { get; set; }


        public int CollectionID { get; set; }


        public int CategoryID { get; set; }





    }
}
