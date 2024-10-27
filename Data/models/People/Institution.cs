using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.People
{
    public class Institution : IBaseEntity<Institution>
    {

        [Key]
        public int InstitutionID { get; set; }

        public required int UserID { get; set; }

        public string? Latitute { get; set; }

        public string? Longitute { get; set; }


        public string? WebsiteURL { get; set; }

        public DateOnly EstablishedYear { get; set; }

    }
}
