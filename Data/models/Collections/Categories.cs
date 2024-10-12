using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class Categories : IBaseEntity<Categories>
    {
        [Key]
        public int CategoryID { get; set; } // Primary Key
        public string CategoryName { get; set; } // Category name with a maximum length of 200 characters

        public ICollection<CollectionsCategories> CollectionCategories { get; set; }

    }

}
