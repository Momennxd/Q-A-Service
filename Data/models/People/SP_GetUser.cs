using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.models.Base;

namespace Data.models.People
{
    public class SP_GetUser :IBaseEntity<SP_GetUser>
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public string? Notes { get; set; }
        public int UserPoints { get; set; }
    }
}
