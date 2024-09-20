using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Repository;

namespace Core_Layer.Entities.Countries
{
    public class eCountryDA : Repository<eCountryDA>
    {

        [Key]
        public int CountryID { get; set; }


        public required string CountryName { get; set; }


    }
}
