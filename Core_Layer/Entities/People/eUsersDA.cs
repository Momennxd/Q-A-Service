using DataAccess_Layer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.People
{
    public class eUsersDA : Repository<eUsersDA>
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int PersonID { get; set; }
        [Required]
        public bool IsACtive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        #region Navigation
        
        
        public virtual ePersonDA Person { get; set; }
        
        
        #endregion



    }
}
