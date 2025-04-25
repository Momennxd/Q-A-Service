using Data.models.Base;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Institutions
{
    public class Institution : IBaseEntity<Institution>
    {
        [Key]
        public int InstitutionID { get; set; }
        public int UserID { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string InstitutionName { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime EstablishedYear { get; set; }

        public virtual User User { get; set; }
    }
}
